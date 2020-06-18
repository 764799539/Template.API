using System;

namespace Template.NuGet
{
    /// <summary>
    /// Redis设置
    /// </summary>
    public class RedisSettings
    {
        private static RedisSettings _Config;

        public string Mode { get; set; }

        public string Domain { get; set; }

        public RedisProviders RedisProviders { get; set; }

        public static RedisSettings Config
        {
            get
            {
                if (_Config == null)
                {
                    _Config = new RedisSettings
                    {
                        Mode = ConfigHelper.GetAppConfig("Redis:Mode"),
                        Domain = ConfigHelper.GetAppConfig("Redis:Domain"),
                        RedisProviders = new RedisProviders()
                        {
                            RedisGeneralProvider = new RedisGeneralProvider
                            {
                                Name = ConfigHelper.GetAppConfig("Redis:Providers:General:Name"),
                                Type = ConfigHelper.GetAppConfig("Redis:Providers:General:Type"),
                                ReadWriteHosts = ConfigHelper.GetAppConfig("Redis:Providers:General:ReadWriteHosts"),
                                ReadOnlyHosts = ConfigHelper.GetAppConfig("Redis:Providers:General:ReadOnlyHosts"),
                                DB = new long?(Convert.ToInt64(ConfigHelper.GetAppConfig("Redis:Providers:General:DB")))
                            },
                            RedisSentinelProvider = new RedisSentinelProvider
                            {
                                Name = ConfigHelper.GetAppConfig("Redis:Providers:Sentinel:Name"),
                                Type = ConfigHelper.GetAppConfig("Redis:Providers:Sentinel:Type"),
                                SentinelHosts = ConfigHelper.GetAppConfig("Redis:Providers:Sentinel:SentinelHosts"),
                                DB = new long?(Convert.ToInt64(ConfigHelper.GetAppConfig("Redis:Providers:Sentinel:DB")))
                            }
                        }
                    };
                }
                return _Config;
            }
        }
    }
}
