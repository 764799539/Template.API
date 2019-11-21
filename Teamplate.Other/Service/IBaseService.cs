using System;
using System.Collections.Generic;
using System.Text;

namespace Teamplate.NuGet
{
    public interface IBaseService : IDisposable
    {
        // Properties
        IServiceFactory ServiceFactory { get; set; }
    }
}
    