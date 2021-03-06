using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TLabs.DotnetHelpers
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PagedList()
        {
        }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            Items = new List<T>(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source,
            int pageNumber, int pageSize, bool isReverse = false)
        {
            var count = source.Count();
            var items = isReverse
                ? source.SkipLast((pageNumber - 1) * pageSize).TakeLast(pageSize).ToList()
                : source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
