using System;
using System.Collections.Generic;
using System.Text;

namespace Teamplate.NuGet
{
    public class RedisSentinelProvider
    {
        // Properties
        public string Name { get; set; }

        public string Type { get; set; }

        public string SentinelHosts { get; set; }

        public long? DB { get; set; }

        public string MasterName { get; set; }
    }

}
