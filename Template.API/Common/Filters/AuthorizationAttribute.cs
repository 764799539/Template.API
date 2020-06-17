using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Template.NuGet;

namespace Template.API
{
    /// <summary>
    /// 权限验证过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        string _UsecaseKey = string.Empty;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="usecaseKey"></param>
        public AuthorizationAttribute(string usecaseKey)
        {
            _UsecaseKey = usecaseKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!AuthManager.CheckUsecase(_UsecaseKey))
            {
                var result = JsonReturn.CreateResult(ResultStatus.Unauthorized, "对不起，您没有此操作权限！");
                ContentResult contentResult = new ContentResult() { Content = JsonHelper.Serialize(result) };
                context.Result = contentResult;
            }
        }
    }
}
