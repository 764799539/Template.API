using System;

namespace Template.NuGet
{
    [Serializable]
    public class PagingParam
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }

        public PagingParam(int page = 1, int pageSize = 10)
        {
            Page = page;
            PageSize = pageSize;
        }

        public PagedData ToPagedData() => new PagedData(this);

        public PagedData<T> ToPagedData<T>() => new PagedData<T>(this);
    }

}
