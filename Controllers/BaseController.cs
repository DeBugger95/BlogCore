using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogCore.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace BlogCore.Controllers
{
    public class BaseController : Controller
    {
        protected readonly Blog_L2Context _db;
        protected readonly IConfiguration _configuration;
        protected readonly IMapper _mapper;
        protected readonly int _hotPostSize;
        protected readonly int _latestPostSize;
        protected readonly int _correlationPostSize;
        protected readonly int _stochasticPostSize;
        protected readonly IMemoryCache _cache;
        protected BaseController(Blog_L2Context blogContext, IMapper mapper, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _db = blogContext;
            _configuration = configuration;
            _hotPostSize = Convert.ToInt32(_configuration.GetSection("Value:HotPostSize").Value);
            _latestPostSize = Convert.ToInt32(_configuration.GetSection("Value:LatestPostSize").Value);
            _correlationPostSize = Convert.ToInt32(_configuration.GetSection("Value:CorrelationPostSize").Value);
            _stochasticPostSize = Convert.ToInt32(_configuration.GetSection("Value:StochasticPostSize").Value);
            _mapper = mapper;
            _cache = memoryCache;

        }

        protected virtual void CategoryInfoAsync()
        {
            ViewBag.Categorys =_db.Category.AsNoTracking().Where(u => u.IsActive == true).OrderBy(u => u.SortNo).Select(u=> u.CategoryName).ToList();
        }

        protected virtual void SetCookie()
        {
            string gid = Guid.NewGuid().ToString();
            //System.Web.HttpCookie = new System.Web.HttpCookie("Blog", gid);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}