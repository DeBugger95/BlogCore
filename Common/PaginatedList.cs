using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Common
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int PageTotal { get; set; }
        public int PageCount { get; set; }
        public bool HasPreViousPage => (PageIndex > 1);
        public bool HasNextPage => (PageIndex < PageTotal);
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageCount = count;
            PageTotal = (int)Math.Ceiling(count / (decimal)pageSize);
            this.AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreatePaginatedAsync(IQueryable<T> query,int pageIndex,int pageSize)
        {
            var count = await query.CountAsync();
            var total = (int)Math.Ceiling(count / (decimal)pageSize);
            if (total == 0) total = 1;
            if (pageIndex < 1) pageIndex = 1;
            if (pageIndex > total) pageIndex = total;
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var output = new PaginatedList<T>(items, count, pageIndex, pageSize);
            return output;
        }
    }
}
