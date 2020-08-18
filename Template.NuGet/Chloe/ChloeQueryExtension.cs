using Chloe;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public static class ChloeQueryExtension
    {
		public static PagedData<T> TakePageData<T>(this IQuery<T> query, PagingParam pagingParam, SortingParam sortingParam = null)
		{
			if (sortingParam != null && !sortingParam.SortString.IsNullOrEmpty())
				query.OrderBy(sortingParam.SortString);
			PagedData<T> PageData = pagingParam.ToPagedData<T>();
			PageData.DataList = query.TakePage(pagingParam.Page, pagingParam.PageSize).ToList();
			PageData.TotalCount = query.Count();
			return PageData;
		}
	}
}
