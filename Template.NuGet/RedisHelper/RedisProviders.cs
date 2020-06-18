using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public class RedisProviders
    {
        public RedisGeneralProvider RedisGeneralProvider { get; set; }
        public RedisSentinelProvider RedisSentinelProvider { get; set; }
    }

}
