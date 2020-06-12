using Microsoft.AspNetCore.Mvc;
using Template.BLL;
using Template.Model;
using Template.NuGet;

namespace Template.API.Controllers
{
    /// <summary>
    /// 用户API
    /// </summary>
    [ApiController]
    [Route("API/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;
        /// <summary>
        /// 用户服务注入
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="commonService"></param>
        public UserController(IUserService userService, ICommonService commonService)
        {
            _userService = userService;
            _commonService = commonService;
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
            Sys_User_LoginAuth userLoginAuth = _commonService.Get<Sys_User_LoginAuth>(sa => sa.Identifier == UserName && sa.Certificate == PassWord && sa.Type == 1 && sa.Status == 0);






            return new JsonReturn<Sys_User> { Status = ResultStatus.OK, Data = new Sys_User() };
        }


    }
}