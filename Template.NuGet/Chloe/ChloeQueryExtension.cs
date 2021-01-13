using Chloe;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public static class ChloeQueryExtension
    {
		public static PagedData<T> TakePageData<T>(this IQuery<T> query, PagingAndSortingParam PagingAndSortingParam)
		{
			if (PagingAndSortingParam != null && !PagingAndSortingParam.SortString.IsNullOrEmpty())
				query.OrderBy(PagingAndSortingParam.SortString);
			PagedData<T> PageData = PagingAndSortingParam.ToPagedData<T>();
			PageData.DataList = query.TakePage(PagingAndSortingParam.Page, PagingAndSortingParam.PageSize).ToList();
			PageData.TotalCount = query.Count();
			return PageData;
		}
	}
}
