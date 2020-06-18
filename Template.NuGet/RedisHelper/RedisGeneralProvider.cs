using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public class RedisGeneralProvider
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string ReadWriteHosts { get; set; }

        public string ReadOnlyHosts { get; set; }

        public long? DB { get; set; }
    }

}
