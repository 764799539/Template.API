using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public interface IBaseService : IDisposable
    {
        IServiceFactory ServiceFactory { get; set; }
    }
}
    