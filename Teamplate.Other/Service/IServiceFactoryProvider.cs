using System;
using System.Collections.Generic;
using System.Text;

namespace Teamplate.Other
{
    public interface IServiceFactoryProvider
    {
        // Properties
        IServiceFactory ServiceFactory { get; set; }
    }
}
