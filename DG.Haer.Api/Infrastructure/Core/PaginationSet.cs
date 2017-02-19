using System;
using System.Collections.Generic;
using System.Linq;

namespace DG.Haer.Api.Core
{
    public class PaginationSet<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
