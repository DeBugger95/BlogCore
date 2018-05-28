using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogCore.Application.Dtos;
using BlogCore.Common;
using BlogCore.Core.Models;
using BlogCore.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace BlogCore.Controllers
{
    public class PostMController : BaseController
    {
        public PostMController(Blog_L2Context blogContext, IMapper mapper, IConfiguration configuration, IMemoryCache memoryCache) : base(blogContext, mapper, configuration, memoryCache)
        {
        }

        public async Task<IActionResult> Index(int page = 1 , int limit = 10,string title="")
        {
            try
            {
                var queryPosts = _db.Post.AsNoTracking().OrderByDescending(u => u.Id);
                if (!string.IsNullOrWhiteSpace(title))
                {
                    queryPosts = queryPosts.Where(u=>u.Title.Contains(title)).OrderByDescending(u => u.Id);
                }
                var postResult = await PaginatedList<Post>.CreatePaginatedAsync(queryPosts, page, limit);
                ViewBag.PageIndex = postResult.PageIndex;
                ViewBag.PageTotal = postResult.PageTotal;
                ViewBag.PageCount = postResult.PageCount;
                ViewBag.HasPreViousPage = postResult.HasPreViousPage;
                ViewBag.HasNextPage = postResult.HasNextPage;
                if (!string.IsNullOrWhiteSpace(HttpContext.Request.QueryString.ToString()))
                {
                    ViewBag.Query = HttpContext.Request.QueryString.ToString().Substring(0);
                }
                ViewBag.PageUrl = "/postm";
                ViewData["SearchTitle"] = title;
                var postMaps = _mapper.Map<List<Post>, List<PostDto>>(postResult);
                ViewBag.Posts = new List<PostDto>();
                if (postMaps.Count > 0)
                {
                    ViewBag.Posts = postMaps;
                }
                if (TempData["ErrorMessage"] != null) ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            finally
            {
                ViewData["PostM"] = "active";
            }
            return View();
        }

    }
}