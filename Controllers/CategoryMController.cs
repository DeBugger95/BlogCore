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
    public class CategoryMController : BaseController
    {
        public CategoryMController(Blog_L2Context blogContext, IMapper mapper, IConfiguration configuration, IMemoryCache memoryCache) : base(blogContext, mapper, configuration, memoryCache)
        {
        }

        public async Task<IActionResult> Index(int page = 1, int limit = 10, string name="")
        {
            try
            {
                var queryCategorys = _db.Category.AsNoTracking().OrderByDescending(u => u.Id);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    queryCategorys = queryCategorys.Where(u => u.CategoryName.Contains(name)).OrderByDescending(u => u.SortNo);
                }
                var categoryResult = await PaginatedList<Category>.CreatePaginatedAsync(queryCategorys, page, limit);
                ViewBag.PageIndex = categoryResult.PageIndex;
                ViewBag.PageTotal = categoryResult.PageTotal;
                ViewBag.PageCount = categoryResult.PageCount;
                ViewBag.HasPreViousPage = categoryResult.HasPreViousPage;
                ViewBag.HasNextPage = categoryResult.HasNextPage;
                if (!string.IsNullOrWhiteSpace(HttpContext.Request.QueryString.ToString()))
                {
                    ViewBag.Query = HttpContext.Request.QueryString.ToString().Substring(0);
                }
                ViewBag.PageUrl = "/categorym";
                ViewData["SearchName"] = name;
                var categoryMaps = _mapper.Map<List<Category>, List<CategoryDto>>(categoryResult);
                ViewBag.Categorys = new List<CategoryDto>();
                if (categoryMaps.Count > 0)
                {
                    ViewBag.Categorys = categoryMaps;
                }
                if (TempData["ErrorMessage"] != null) ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            finally
            {
                ViewData["CategoryM"] = "active";
            }
            return View();
        }

        public IActionResult Create()
        {
            if (TempData["ErrorMessage"] != null) ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            ViewData["CategoryM"] = "active";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto input)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(input.CategoryName))
                {
                    var queryCategory = await _db.Category.AsNoTracking().Where(u => u.CategoryName.ToLower().Trim() == input.CategoryName.ToLower().Trim()).SingleOrDefaultAsync();
                    if(queryCategory == null)
                    {
                        Category category = new Category()
                        {
                            CategoryName = input.CategoryName,
                            IsActive = input.IsActive,
                            CreateDate = DateTime.Now,
                            SortNo = input.SortNo
                        };
                        _db.Category.Add(category);
                        await _db.SaveChangesAsync();
                        return Redirect("/categorym");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "CategoryName Is Error";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "CategoryName Is Null";
                }
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
             return Redirect("/categorym/create");
        }

        public IActionResult Delete()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(input.CategoryName))
                {
                    var queryCategory = await _db.Category.AsNoTracking().Where(u => u.CategoryName.ToLower().Trim() == input.CategoryName.ToLower().Trim()).SingleOrDefaultAsync();
                    if (queryCategory == null)
                    {
                        Category category = new Category()
                        {
                            CategoryName = input.CategoryName,
                            IsActive = input.IsActive,
                            CreateDate = DateTime.Now,
                            SortNo = input.SortNo
                        };
                        _db.Category.Add(category);
                        await _db.SaveChangesAsync();
                        return Redirect("/categorym");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "CategoryName Is Error";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "CategoryName Is Null";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return Redirect("/categorym/create");
        }
    }
}