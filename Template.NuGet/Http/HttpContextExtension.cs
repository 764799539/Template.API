using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public static class HttpContextExtension
    {
        public static string Content(this HttpContext httpContext, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
            {
                return null;
            }
            if (contentPath[0] != '~')
            {
                return contentPath;
            }
            PathString str = new PathString(contentPath.Substring(1));
            return httpContext.Request.PathBase.Add(str).Value;
        }

        //public static bool IsAjaxRequest(this HttpContext httpContext) => httpContext.Request.IsAjaxRequest;
        public static bool IsHttps(this HttpContext httpContext) => httpContext.Request.IsHttps;

        public static HttpContext Current =>
            ((IHttpContextAccessor)CommonHelper.ServiceProvider.GetService(Type.GetTypeFromHandle(typeof(IHttpContextAccessor).TypeHandle))).HttpContext;
    }

}
