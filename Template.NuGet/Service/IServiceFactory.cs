using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public interface IServiceFactory : IDisposable
    {
        T GetService<T>() where T : IBaseService;
    }
}
