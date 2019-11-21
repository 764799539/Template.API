using System;
using System.Collections.Generic;
using System.Text;

namespace Teamplate.Other
{
    [Serializable]
    public class PagingParam
    {
        // Fields
        private int _Page;
        private int _PageSize;

        // Methods
        public PagingParam()
        {
            this._Page = 1;
            this._PageSize = 10;
        }

        public PagingParam(int page, int pageSize)
        {
            this._Page = 1;
            this._PageSize = 10;
            this.Page = page;
            this.PageSize = pageSize;
        }

        public PagedData ToPagedData() =>
            new PagedData(this);

        public PagedData<T> ToPagedData<T>() =>
            new PagedData<T>(this);

        // Properties
        public int Page { get; set; }

        public int PageSize { get; set; }
    }

}
