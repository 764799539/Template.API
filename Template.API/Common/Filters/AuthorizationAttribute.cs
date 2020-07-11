using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceStack;
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
        /// 标记传入授权标记
        /// </summary>
        /// <param name="usecaseKey">授权标记</param>
        public AuthorizationAttribute(string usecaseKey)
        {
            _UsecaseKey = usecaseKey;
        }

        /// <summary>
        /// 控制器中加了该标记的方法中代码执行之前该方法，所以可以用做权限校验。
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //if (!AuthManager.CheckUsecase(_UsecaseKey))
            //{
            //    var result = JsonReturn.CreateResult(ResultStatus.Unauthorized, "对不起，您没有此操作权限！");
            //    ContentResult contentResult = new ContentResult() { Content = JsonHelper.Serialize(result) };
            //    context.Result = contentResult;
            //}
        }
        ///// <summary>
        ///// 控制器中加了该属性的方法执行完成后才会来执行该方法。
        ///// </summary>
        ///// <param name="context"></param>
        //public override void OnActionExecuted(ActionExecutedContext context)
        //{

        //}
        ///// <summary>
        ///// OnActionExecuted()方法完成后执行。
        ///// </summary>
        ///// <param name="context"></param>
        ///// <param name="next"></param>
        ///// <returns></returns>
        //public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        //{
        //    return base.OnResultExecutionAsync(context, next);
        //}
    }
}
