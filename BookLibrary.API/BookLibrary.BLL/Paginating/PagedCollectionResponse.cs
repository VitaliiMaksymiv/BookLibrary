using System;
using System.Collections.Generic;

namespace BookLibrary.BLL.Paginating
{
    public class PagedCollectionResponse<T> where T : class, new()
    {
        public IEnumerable<T> Items { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
    }
}