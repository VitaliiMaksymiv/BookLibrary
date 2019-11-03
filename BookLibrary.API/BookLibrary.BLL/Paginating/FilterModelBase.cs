using System;

namespace BookLibrary.BLL.Paginating
{
    public abstract class FilterModelBase : ICloneable
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public FilterModelBase()
        {
            this.Page = 1;
            this.PageSize = 100;
        }

        public abstract object Clone();
    }
}
