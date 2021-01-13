using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template.API.Common.Middleware
{
    /// <summary>
    /// 约定中间件模式的中间件类
    /// </summary>
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _context;
        private bool _isToNext;

        /// <summary>
        /// 约定构造，也需要在Configure方法内使用UseMiddleware注册，传入非依赖注入可注入的其他参数
        /// </summary>
        /// <param name="next">必须参数</param>
        /// <param name="context">可选</param>
        /// <param name="isToNext">可选</param>
        public TestMiddleware(RequestDelegate next, string context, bool isToNext = false)
        {
            _next = next;
            _context = context;
            _isToNext = isToNext;
        }

        /// <summary>
        /// 约定方法
        /// </summary>
        /// <param name="HttpContext">必须参数</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext HttpContext)
        {
            await HttpContext.Response.WriteAsync("TestMiddleware");
            if (_isToNext) await _next(HttpContext);
        }
    }
}
