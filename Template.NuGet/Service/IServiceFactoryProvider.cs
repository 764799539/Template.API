using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public interface IServiceFactoryProvider
    {
        // Properties
        IServiceFactory ServiceFactory { get; set; }
    }
}
