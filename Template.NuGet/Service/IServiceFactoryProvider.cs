using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public interface IServiceFactoryProvider
    {
        IServiceFactory ServiceFactory { get; set; }
    }
}
