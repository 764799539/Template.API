using System;
using System.Collections.Generic;
using System.Text;

namespace Teamplate.NuGet
{
    public class RedisGeneralProvider
    {
        // Properties
        public string Name { get; set; }

        public string Type { get; set; }

        public string ReadWriteHosts { get; set; }

        public string ReadOnlyHosts { get; set; }

        public long? DB { get; set; }
    }

}
