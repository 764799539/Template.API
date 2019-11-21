using System;
using System.Collections.Generic;
using System.Text;

namespace Teamplate.Other
{
    public interface IServiceFactory : IDisposable
    {
        // Methods
        T GetService<T>() where T : IBaseService;
    }
}
