using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public interface IRedis
    {
        void Start();

        PooledRedisClientManager RedisClientsManager { get; }
    }

}
