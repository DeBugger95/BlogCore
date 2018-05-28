using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogCore.Application.Dtos;
using BlogCore.Common;
using BlogCore.Core.Models;
using BlogCore.Core.ViewModels;
using BlogCore.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace BlogCore.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(Blog_L2Context blogContext, IMapper mapper, IConfiguration configuration, IMemoryCache memoryCache) : base(blogContext, mapper, configuration, memoryCache)
        {
        }

       
        public async Task<IActionResult> Index()
        {
            try
            {
                CategoryInfoAsync();
                GetInfo getInfo = new GetInfo(_db, _configuration,_cache);
                #region hot posts 5
                var hotPostResult =await getInfo.GetHotPostInfoAsync();
                var hotPostMaps = _mapper.Map<List<Post>, List<PostDto>>(hotPostResult);
                ViewBag.HotPosts = new List<PostDto>();
                if (hotPostMaps.Count > 0)
                {
                    ViewBag.HotPosts = hotPostMaps;
                }
                #endregion
                #region latest posts 3            
                var latestPostResult = await getInfo.GetLatestPostInfoAsync();
                var latestPostMaps = _mapper.Map<List<Post>, List<PostDto>>(latestPostResult);
                ViewBag.LatestPosts = new List<PostDto>();
                if (latestPostMaps.Count > 0) ViewBag.LatestPosts = latestPostMaps;
                #endregion
                #region slider posts 3
                var stochasticPostResult = await getInfo.GetStochasticPostInfoAsync();
                var stochasticPostMaps = _mapper.Map<List<Post>, List<PostDto>>(stochasticPostResult);
                ViewBag.StochasticPosts = new List<PostDto>();
                if (stochasticPostMaps.Count > 0)
                {
                    ViewBag.StochasticPosts = stochasticPostMaps;
                }

                #endregion
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("/Error");
            }
            finally
            {
                ViewData["Home"] = "active";
                _db.Dispose();
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            ViewData["About"] = "active";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            if(TempData["Error"] !=null)
            {
                ViewData["Message"] = TempData["Error"];
            }
            return View();
        }
    }
}
