using System;
using System.Collections.Generic;
using System.Text;

namespace Teamplate.NuGet
{
    public class RedisSettings
    {
        // Fields
        private static RedisSettings _Config;

        // Properties
        public static RedisSettings Config
        {
            get
            {
                if (_Config == null)
                {
                    RedisSettings settings1 = new RedisSettings
                    {
                        Mode = ConfigHelper.GetAppConfig("Redis:Mode"),
                        Domain = ConfigHelper.GetAppConfig("Redis:Domain")
                    };
                    RedisProviders providers1 = new RedisProviders();
                    RedisGeneralProvider provider1 = new RedisGeneralProvider
                    {
                        Name = ConfigHelper.GetAppConfig("Redis:Providers:General:Name"),
                        Type = ConfigHelper.GetAppConfig("Redis:Providers:General:Type"),
                        ReadWriteHosts = ConfigHelper.GetAppConfig("Redis:Providers:General:ReadWriteHosts"),
                        ReadOnlyHosts = ConfigHelper.GetAppConfig("Redis:Providers:General:ReadOnlyHosts"),
                        DB = new long?(Convert.ToInt64(ConfigHelper.GetAppConfig("Redis:Providers:General:DB")))
                    };
                    providers1.RedisGeneralProvider = provider1;
                    RedisSentinelProvider provider2 = new RedisSentinelProvider
                    {
                        Name = ConfigHelper.GetAppConfig("Redis:Providers:Sentinel:Name"),
                        Type = ConfigHelper.GetAppConfig("Redis:Providers:Sentinel:Type"),
                        SentinelHosts = ConfigHelper.GetAppConfig("Redis:Providers:Sentinel:SentinelHosts"),
                        DB = new long?(Convert.ToInt64(ConfigHelper.GetAppConfig("Redis:Providers:Sentinel:DB")))
                    };
                    providers1.RedisSentinelProvider = provider2;
                    settings1.RedisProviders = providers1;
                    _Config = settings1;
                }
                return _Config;
            }
        }

        public string Mode { get; set; }

        public string Domain { get; set; }

        public RedisProviders RedisProviders { get; set; }
    }

}
