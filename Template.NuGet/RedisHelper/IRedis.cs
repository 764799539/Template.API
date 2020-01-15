﻿using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public interface IRedis
    {
        // Methods
        void Start();

        // Properties
        PooledRedisClientManager RedisClientsManager { get; }
    }

}