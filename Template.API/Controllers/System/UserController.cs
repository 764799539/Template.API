using Microsoft.AspNetCore.Mvc;
using Template.BLL;
using Template.Model;
using Template.NuGet;

namespace Template.API.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [ApiController]
    [Route("API/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        /// <summary>
        /// 用户服务注入
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <returns></returns>
        [HttpPost, Route("Login")]
        public JsonReturn<Sys_User> Login(string UserName,string PassWord) {
            if (string.IsNullOrEmpty(UserName))
                return new JsonReturn<Sys_User> { Status = ResultStatus.Failed, Msg = "用户名或密码不能为空！" };
            if (string.IsNullOrEmpty(PassWord))
                return new JsonReturn<Sys_User> { Status = ResultStatus.Failed, Msg = "密码不能为空！" };

            return new JsonReturn<Sys_User> { Status = ResultStatus.OK, Data = new Sys_User() };
        }


    }
}