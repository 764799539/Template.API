using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public class RedisProviders
    {
        // Properties
        public RedisGeneralProvider RedisGeneralProvider { get; set; }

        public RedisSentinelProvider RedisSentinelProvider { get; set; }
    }

}
