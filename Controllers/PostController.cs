using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogCore.Application.Dtos;
using BlogCore.Application.EnumsType;
using BlogCore.Common;
using BlogCore.Core.Models;
using BlogCore.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace BlogCore.Controllers
{
    public class PostController : BaseController
    {
        public PostController(Blog_L2Context blogContext, IMapper mapper, IConfiguration configuration, IMemoryCache memoryCache) : base(blogContext, mapper, configuration, memoryCache)
        {
        }

        public async Task<IActionResult> Index(SearchInfo input, int page = 1)
        {
            try
            {
                if (input == null) input = new SearchInfo();
                CategoryInfoAsync();
                GetInfo getInfo = new GetInfo(_db, _configuration, _cache);
                #region latest posts 10         
                var latestPostResult = await getInfo.GetLatestPostInfoAsync(page, 10, input.Title, input.Category);
                var latestPostMaps = _mapper.Map<List<Post>, List<PostDto>>(latestPostResult);
                ViewBag.LatestPosts = new List<PostDto>();
                if (latestPostMaps.Count > 0) ViewBag.LatestPosts = latestPostMaps;
                #endregion
                #region page
                ViewBag.Query = "";
                if (!string.IsNullOrWhiteSpace(HttpContext.Request.QueryString.ToString()))
                {
                    ViewBag.Query = string.Join('&', HttpContext.Request.QueryString.ToString().Substring(0).Split('&').Where(u => !u.Contains("page")));
                }
                ViewBag.PageIndex = latestPostResult.PageIndex;
                ViewBag.PageTotal = latestPostResult.PageTotal;
                ViewBag.PageCount = latestPostResult.PageCount;
                ViewBag.HasPreViousPage = latestPostResult.HasPreViousPage;
                ViewBag.HasNextPage = latestPostResult.HasNextPage;
                ViewBag.PageUrl = "/post";
                ViewData["SearchTitle"] = input.Title;
                ViewData["Category"] = input.Category;
                #endregion
                #region hot posts 5
                var hotPostResult = await getInfo.GetHotPostInfoAsync();
                var hotPostMaps = _mapper.Map<List<Post>, List<PostDto>>(hotPostResult);
                ViewBag.HotPosts = new List<PostDto>();
                if (hotPostMaps.Count > 0)
                {
                    ViewBag.HotPosts = hotPostMaps;
                }
                #endregion
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("/home/error");
            }
            finally
            {
                ViewData["Post"] = "active";
                _db.Dispose();
            }
            return View();
        }

        public async Task<IActionResult> Detail(int pid)
        {
            try
            {
                CategoryInfoAsync();
                GetInfo getInfo = new GetInfo(_db, _configuration, _cache);
                #region correlation posts 5
                var correlationPostResult = await getInfo.GetCorrelationPostInfoAsync(pid);
                var correlationPostMaps = _mapper.Map<List<Post>, List<PostDto>>(correlationPostResult);
                ViewBag.CorrelationPosts = new List<PostDto>();
                if (correlationPostMaps.Count > 0)
                {
                    ViewBag.CorrelationPosts = correlationPostMaps;
                }

                #endregion
                var queryPost = await _db.Post.Where(u => u.Id == pid).Include(u => u.PostTag).SingleOrDefaultAsync();
                queryPost.Views += 1;
                if (queryPost == null)
                {
                    TempData["Error"] = "文章不存在.";
                    return Redirect("/home/error");
                }
                var postMaps = _mapper.Map<Post, PostDetailDto>(queryPost);
                var queryTags = from a in queryPost.PostTag
                                join b in _db.Tag.AsNoTracking()
                                on a.TagId equals b.Id
                                where a.PostId == pid && b.IsActive == true
                                orderby b.SortNo ascending
                                select b;
                var tagMaps = _mapper.Map<List<Tag>, List<TagDto>>(queryTags.ToList());
                postMaps.Tags = tagMaps;
                var output = postMaps;
                _db.Post.Attach(queryPost);
                _db.Entry(queryPost).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return View(output);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("/home/error");
            }
            finally
            {
                _db.Dispose();
            }
        }
        public class SearchInfo
        {
            public string Title { get; set; } = "";
            public string Category { get; set; } = "";
        }
    }
}