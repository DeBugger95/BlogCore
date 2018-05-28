using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogCore.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace BlogCore.Controllers
{
    public class GAdminController : BaseController
    {
        public GAdminController(Blog_L2Context blogContext, IMapper mapper, IConfiguration configuration, IMemoryCache memoryCache) : base(blogContext, mapper, configuration, memoryCache)
        {
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewData["GAdmin"] = "active";
            return View();
        }
    }
}