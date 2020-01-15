using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Template.NuGet;

namespace Template.API.Controllers
{
    /// <summary>
    /// 自定义控制器基类
    /// </summary>
    public abstract class BaseController : Controller
    {
        static readonly Type TypeOfCurrent = typeof(BaseController);
        static readonly Type TypeOfDisposableAttribute = typeof(DisposableAttribute);

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            DisposeMembers();
        }
        /// <summary>
        /// 扫描对象内所有带有 DisposableAttribute 标记并实现了 IDisposable 接口的属性和字段，执行其 Dispose() 方法
        /// </summary>
        void DisposeMembers()
        {
            Type type = GetType();

            List<PropertyInfo> properties = new List<PropertyInfo>();
            List<FieldInfo> fields = new List<FieldInfo>();

            Type searchType = type;

            while (true)
            {
                properties.AddRange(searchType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(a =>
                {
                    if (typeof(IDisposable).IsAssignableFrom(a.PropertyType))
                    {
                        return a.CustomAttributes.Any(c => c.AttributeType == TypeOfDisposableAttribute);
                    }

                    return false;
                }));

                fields.AddRange(searchType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(a =>
                {
                    if (typeof(IDisposable).IsAssignableFrom(a.FieldType))
                    {
                        return a.CustomAttributes.Any(c => c.AttributeType == TypeOfDisposableAttribute);
                    }

                    return false;
                }));

                if (searchType == TypeOfCurrent)
                    break;
                else
                    searchType = searchType.BaseType;
            }

            foreach (var pro in properties)
            {
                IDisposable val = pro.GetValue(this) as IDisposable;
                if (val != null)
                    val.Dispose();
            }

            foreach (var field in fields)
            {
                IDisposable val = field.GetValue(this) as IDisposable;
                if (val != null)
                    val.Dispose();
            }
        }

        //[Disposable]
        //ServiceFactory _serviceFactory;
        //IServiceFactory ServiceFactory
        //{
        //    get
        //    {
        //        if (_serviceFactory == null)
        //            _serviceFactory = new ServiceFactory(HttpContext.RequestServices);
        //        return _serviceFactory;
        //    }
        //}

        //protected T GetService<T>() where T : IBaseService
        //{
        //    return ServiceFactory.GetService<T>();
        //}
    }
}
