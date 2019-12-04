using Microsoft.Extensions.Configuration;
using System;

namespace Template.NuGet
{
    public static class ConfigHelper
    {
        // Methods
        public static string GetAppConfig(string key)
        {
            string str;
            try
            {
                str = Configuration[key];
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return str;
        }

        // Properties
        public static IConfiguration Configuration { get; set; }
    }


}
