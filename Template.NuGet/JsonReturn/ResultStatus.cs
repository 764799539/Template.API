using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    public enum ResultStatus
    {
        OK = 100,
        NotMeetRequirement = 105,
        Failed = 101,
        Unauthorized = 103,
        NotLogin = 102
    }

}
