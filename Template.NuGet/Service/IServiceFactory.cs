using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public interface IServiceFactory : IDisposable
    {
        // Methods
        T GetService<T>() where T : IBaseService;
    }
}
