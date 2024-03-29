﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Template.NuGet
{
    /// <summary>
    /// 服务注册类
    /// </summary>
    public static class ServiceRegistry
    {
        private static void RegisterAppServices(IServiceCollection services, List<Type> implementationTypes)
        {
            Type appServiceType = typeof(IBaseService);
            foreach (Type type in implementationTypes)
            {
                foreach (Type type2 in from a in IntrospectionExtensions.GetTypeInfo(type).ImplementedInterfaces select a)
                {
                    if (typeof(IDisposable).IsAssignableFrom(type))
                    {
                        try
                        {
                            ServiceCollectionServiceExtensions.AddScoped(services, type2, type);
                        }
                        catch (Exception)
                        {
                        }
                        
                    }
                    else
                    {
                        try
                        {
                            ServiceCollectionServiceExtensions.AddTransient(services, type2, type);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }

        public static void RegisterAppServices(this IServiceCollection services, Assembly assembly)
        {
            List<Type> implementationTypes = ServiceTypeFinder.Find(assembly);
            RegisterAppServices(services, implementationTypes);
        }

        public static void RegisterAppServicesFromDirectory(this IServiceCollection services, string path)
        {
            List<Type> implementationTypes = ServiceTypeFinder.FindFromDirectory(path);
            RegisterAppServices(services, implementationTypes);
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            List<Type> implementationTypes = ServiceTypeFinder.FindFromCompileLibraries();
            RegisterAppServices(services, implementationTypes);
        }
    }

}
