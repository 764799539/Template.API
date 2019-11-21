using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Teamplate.NuGet
{
    public class RedisGeneral : IRedis
    {
        // Fields
        private PooledRedisClientManager _redisClientsManager;

        // Methods
        public RedisGeneral()
        {
            RedisGeneralProvider redisGeneralProvider = RedisSettings.Config.RedisProviders.RedisGeneralProvider;
            if (redisGeneralProvider == null)
            {
                throw new Exception("not found general node.");
            }
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            if (!string.IsNullOrWhiteSpace(redisGeneralProvider.ReadWriteHosts))
            {
                string[] strArray = redisGeneralProvider.ReadWriteHosts.Split(",".ToCharArray(), (StringSplitOptions)StringSplitOptions.RemoveEmptyEntries);
                list.AddRange(strArray);
            }
            if (!string.IsNullOrWhiteSpace(redisGeneralProvider.ReadOnlyHosts))
            {
                string[] strArray2 = redisGeneralProvider.ReadOnlyHosts.Split(",".ToCharArray(), (StringSplitOptions)StringSplitOptions.RemoveEmptyEntries);
                list2.AddRange(strArray2);
            }
            if ((list2.Count == 0) && (list.Count > 0))
            {
                list2.AddRange((IEnumerable<string>)list);
            }
            this._redisClientsManager = new PooledRedisClientManager((IEnumerable<string>)list, (IEnumerable<string>)list2, redisGeneralProvider.DB.GetValueOrDefault());
        }

        public void Start()
        {
        }

        // Properties
        public PooledRedisClientManager RedisClientsManager =>
            this._redisClientsManager;
    }

}
