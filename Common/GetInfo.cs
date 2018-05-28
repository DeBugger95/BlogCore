using AutoMapper;
using BlogCore.Controllers;
using BlogCore.Core.Models;
using BlogCore.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Common
{
    public class GetInfo : BaseController
    {
        private static IQueryable<Post> _queryPosts;
        private static List<File> _listFiles;

        public GetInfo(Blog_L2Context blogContext,IConfiguration configuration, IMemoryCache memoryCache) : base(blogContext, null, configuration, memoryCache)
        {
            _queryPosts = _db.Post.AsNoTracking().Where(u => u.IsActive == true);
            _listFiles = _db.File.AsNoTracking().ToList();
        }

        public async Task<List<Post>> GetHotPostInfoAsync()
        {
            var queryHotPosts = _queryPosts.OrderByDescending(u => u.Views).ThenByDescending(u => u.PostDate);
            return await PaginatedList<Post>.CreatePaginatedAsync(queryHotPosts, 1, _hotPostSize);
        }

        public async Task<PaginatedList<Post>> GetLatestPostInfoAsync(int page = 1, int limit = 0, string title = "", string category = "")
        {
            var queryLatestPosts = _queryPosts.OrderByDescending(u => u.PostDate).ThenByDescending(u => u.Views);
            if (!string.IsNullOrWhiteSpace(title))
            {
                queryLatestPosts = queryLatestPosts.Where(u => u.Title.Contains(title)).OrderByDescending(u => u.PostDate).ThenByDescending(u => u.Views);
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                var queryCategory = await (from a in _db.Category.AsNoTracking()
                                           join b in _db.PostCategory.AsNoTracking()
                                           on a.Id equals b.CategoryId
                                           where a.CategoryName.Equals(category) && a.IsActive == true
                                           select new
                                           {
                                               b.PostId
                                           }).ToListAsync();
                List<int> lists = new List<int>();
                foreach(var item in queryCategory)
                {
                    lists.Add(Convert.ToInt32(item.PostId));
                }
                queryLatestPosts = queryLatestPosts.Where(u => lists.Contains(u.Id)).OrderByDescending(u => u.PostDate).ThenByDescending(u => u.Views);
            }
            if (limit == 0) limit = _latestPostSize;
            return await PaginatedList<Post>.CreatePaginatedAsync(queryLatestPosts, page, limit);
        }

        public async Task<List<Post>> GetStochasticPostInfoAsync()
        {
            var queryHotPosts = _queryPosts.OrderByDescending(u => Guid.NewGuid());
            return await PaginatedList<Post>.CreatePaginatedAsync(queryHotPosts, 1, _stochasticPostSize);
        }

        public async Task<List<Post>> GetCorrelationPostInfoAsync(int pid)
        {
            var queryPost = await _db.Post.AsNoTracking().Where(u => u.Id == pid).Include(u => u.PostCategory).SingleOrDefaultAsync();
            List<int> categoryLists = queryPost.PostCategory.Select(u => u.CategoryId).ToList();
            List<int> postLists = await _db.PostCategory.Where(u => u.PostId != pid && categoryLists.Contains(u.CategoryId)).Select(u => u.PostId).OrderBy(u => u).Distinct().ToListAsync();
            var queryCorrelationPosts = _queryPosts.Where(u => postLists.Contains(u.Id)).OrderByDescending(u => u.Views).ThenByDescending(u => u.PostDate);
            return await PaginatedList<Post>.CreatePaginatedAsync(queryCorrelationPosts, 1, _correlationPostSize);
        }
    }
}
