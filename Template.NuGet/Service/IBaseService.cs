using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public interface IBaseService : IDisposable
    {
        // Properties
        IServiceFactory ServiceFactory { get; set; }
    }
}
    