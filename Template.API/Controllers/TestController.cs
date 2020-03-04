using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Template.NuGet;
using Template.BLL;
using Template.Model;

namespace Template.API.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [ApiController]
    [Route("API/[controller]")]
    public class TestController : BaseController
    {
        private readonly IUserService _userService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        public TestController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// 测试借口
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpGet, HttpPost, Route("GetTestContent")]
        public JsonReturn<Sys_User> GetTestContent(string text) => new JsonReturn<Sys_User> { Data = _userService.GetTestContent(text), Status = ResultStatus.OK, Msg = "" };

    }
}
