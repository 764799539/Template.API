using System;
using System.Collections.Generic;
using System.Text;

namespace Template.NuGet
{
    class TestClass
    {
        public JsonReturn ghf() {
            return new JsonReturn<dynamic> { Data = new { }, Status = ResultStatus.OK, Msg = "" };
        }
    }
}
