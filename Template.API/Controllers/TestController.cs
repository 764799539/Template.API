using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.BLL;
using Template.Model;
using Template.NuGet;

namespace Template.API.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [ApiController]
    //[Authorize(Policy = "User")]
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
        [Authorization("AIStore_Search")]
        public JsonReturn<dynamic> GetTestContent(string text)
        {
            long ID = SnowFlakeHelper.NextId();
            return new JsonReturn<dynamic> { Data = _userService.GetTestContent(text), Status = ResultStatus.OK, Msg = "" };
        }

    }
}
