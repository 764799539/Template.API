using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Template.NuGet;

namespace Template.API.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [ApiController]
    [Route("API/[controller]")]
    public class TestController : BaseController
    {
        /// <summary>
        /// 测试借口
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpGet, HttpPost, Route("GetTestContent")]
        public JsonReturn<string> GetTestContent(string text) => new JsonReturn<string> { Data = text, Status = ResultStatus.OK, Msg = "" };
    }
}
