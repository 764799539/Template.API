using Microsoft.AspNetCore.Mvc;
using System;
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
        /// <param name="LoginType">登录类型(1用户名 2手机号 3邮箱 4QQ 5微信 6腾讯微博 7新浪微博)</param>
        /// <returns></returns>
        [HttpPost,HttpGet, Route("Login")]
        public JsonReturn<dynamic> Login(string UserName,string PassWord,int LoginType = 1) {
            try
            {
                if (string.IsNullOrEmpty(UserName))
                    return new JsonReturn<dynamic> { Status = ResultStatus.Failed, Msg = "用户名不能为空！" };
                if (string.IsNullOrEmpty(PassWord))
                    return new JsonReturn<dynamic> { Status = ResultStatus.Failed, Msg = "密码不能为空！" };
                Sys_User_LoginAuth UserLoginAuth = _commonService.Get<Sys_User_LoginAuth>(sa => sa.Identifier == UserName && sa.Certificate == PassWord && sa.Type == LoginType && sa.Status == 0);
                if (UserLoginAuth == null)
                    return new JsonReturn<dynamic> { Status = ResultStatus.Failed, Msg = "用户名或密码错误！" };
                Sys_User User = _commonService.Get<Sys_User>(UserLoginAuth.UserID);
                if (User == null)
                    return new JsonReturn<dynamic> { Status = ResultStatus.Failed, Msg = "您的信息异常，请联系客服！" };
                if (User.Status == (int)UserStatusEnum.Ban)
                    return new JsonReturn<dynamic> { Status = ResultStatus.Failed, Msg = "您的账号已被封禁！" };
                if (User.Status == (int)UserStatusEnum.Delete)
                    return new JsonReturn<dynamic> { Status = ResultStatus.Failed, Msg = "您的账号已被删除！" };
                JwtToken token = new JwtTokenBuilder()
                                    .AddSecurityKey(JwtSecurityKey.Create(ConfigHelper.GetAppConfig("Authorization:SecretKey")))
                                    .AddSubject(ConfigHelper.GetAppConfig("Authorization:Subject"))
                                    .AddIssuer(ConfigHelper.GetAppConfig("Authorization:Issuer"))
                                    .AddAudience(ConfigHelper.GetAppConfig("Authorization:Audience"))
                                    .AddClaim("UserID", User.ID.ToString())
                                    .AddClaim("UserName", User.NickName)
                                    .AddExpiry(Convert.ToInt32(ConfigHelper.GetAppConfig("Authorization:Expiry")))
                                    .Build(); 
                User.Token = token.Value;
                var result = new { User };
                return new JsonReturn<dynamic> { Status = ResultStatus.OK, Data = result };
            }
            catch (Exception ex)
            {
                return new JsonReturn<dynamic> { Status = ResultStatus.Failed, Msg = ex.InnerException.ToString() };
            }
        }


    }
}