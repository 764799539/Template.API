using System;
using System.Collections.Generic;
using System.Text;

namespace Teamplate.NuGet
{
    public interface IServiceFactoryProvider
    {
        // Properties
        IServiceFactory ServiceFactory { get; set; }
    }
}
