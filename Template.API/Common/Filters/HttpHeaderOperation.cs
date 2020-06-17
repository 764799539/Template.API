using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

namespace Template.API
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpHeaderOperation : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }
         
			var actionAttrs = context.MethodInfo.DeclaringType.GetCustomAttributes(true).Union(context.MethodInfo.GetCustomAttributes(true)).OfType<AuthorizeAttribute>();
			         
            var isAuthorized = actionAttrs.Any(a => a.GetType() == typeof(AuthorizeAttribute));

            if (isAuthorized == false) //提供action都没有权限特性标记，检查控制器有没有
            {
				var controllerAttrs = context.MethodInfo.DeclaringType.GetCustomAttributes(true).Union(context.MethodInfo.GetCustomAttributes(true)).OfType<AuthorizeAttribute>();

                isAuthorized = controllerAttrs.Any(a => a.GetType() == typeof(AuthorizeAttribute));
            }

            var isAllowAnonymous = actionAttrs.Any(a => a.GetType() == typeof(AllowAnonymousAttribute));

            if (isAuthorized && isAllowAnonymous == false)
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "Authorization",  //添加Authorization头部参数
                    In = ParameterLocation.Header,
                    //Type = "string",
                    Required = false
                });
            }
        }
    }
}
