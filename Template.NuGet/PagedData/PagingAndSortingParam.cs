using System;

namespace Template.NuGet
{
    public class PagingAndSortingParam
    {
        public PagingAndSortingParam(int page = 1, int pageSize = 10)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public string? SortName { get; set; }

        public string? SortOrder { get; set; }

        public string SortString
        {
            get
            {
                return !string.IsNullOrEmpty(SortName) && !string.IsNullOrEmpty(SortOrder) ? (SortName + " " + SortOrder) : "";
            }
        }

        public PagedData ToPagedData() => new PagedData(this);

        public PagedData<T> ToPagedData<T>() => new PagedData<T>(this);

    }

}
