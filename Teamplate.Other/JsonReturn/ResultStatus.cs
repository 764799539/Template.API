using System;
using System.Collections.Generic;
using System.Text;

namespace Teamplate.NuGet
{
    public enum ResultStatus
    {
        OK = 100,
        Failed = 0x65,
        NotLogin = 0x66,
        Unauthorized = 0x67
    }

}
