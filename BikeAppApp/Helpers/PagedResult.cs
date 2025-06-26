using System;
using System.Collections.Generic;

namespace BikeAppApp.Helpers
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();  // Initialize list to avoid null issues
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
