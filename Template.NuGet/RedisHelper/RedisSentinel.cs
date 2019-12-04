using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public class RedisSentinel : IRedis
    {
        // Fields
        private PooledRedisClientManager _redisClientsManager;
        private readonly ServiceStack.Redis.RedisSentinel _redisSentinel;

        // Methods
        public RedisSentinel()
        {
            RedisSentinelProvider sentinelConfig = RedisSettings.Config.RedisProviders.RedisSentinelProvider;
            if (sentinelConfig == null)
            {
                throw new Exception("not found sentinel node.");
            }
            string[] strArray = sentinelConfig.SentinelHosts.Split(",".ToCharArray(), (StringSplitOptions)StringSplitOptions.RemoveEmptyEntries);
            string str = string.IsNullOrWhiteSpace(sentinelConfig.MasterName) ? null : sentinelConfig.MasterName;
            this._redisSentinel = new ServiceStack.Redis.RedisSentinel(strArray, str);
            this._redisSentinel.RedisManagerFactory = delegate (string[] masters, string[] slaves) {
                return new PooledRedisClientManager(masters, slaves, sentinelConfig.DB.GetValueOrDefault());
            };
        }

        public void Start()
        {
            this._redisClientsManager = _redisSentinel.Start() as PooledRedisClientManager;
        }

        // Properties
        public PooledRedisClientManager RedisClientsManager =>
            this._redisClientsManager;
    }

}
