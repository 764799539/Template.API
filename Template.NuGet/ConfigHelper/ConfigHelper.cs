using Microsoft.Extensions.Configuration;
using System;

namespace Template.NuGet
{
    public static class ConfigHelper
    {
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 获取配置内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppConfig(string key)
        {
            string str;
            try
            {
                str = Configuration[key];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
    }


}
