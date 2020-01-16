using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public class ServiceFactory : IServiceFactory, IDisposable
    {
        private IServiceProvider _ServiceProvider;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
        }

        public void Dispose()
        {
        }

        public virtual T GetService<T>() where T : IBaseService
        {
            object service = _ServiceProvider.GetService(typeof(T));
            if (service == null)
                throw new Exception("Can not find the service.");
            T local1 = (T)service;
            IServiceFactoryProvider provider = local1 as IServiceFactoryProvider;
            if (provider != null)
                provider.ServiceFactory = this;
            return local1;
        }
    }
}
