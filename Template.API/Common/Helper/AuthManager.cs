using System;
using System.Linq;
using System.Collections.Generic;
using Template.NuGet;
using Microsoft.AspNetCore.Http;
using Template.Model;
using Template.Common;


namespace Template.API
{
    /// <summary>
    /// 用户权限管理类
    /// </summary>
    public static class AuthManager
    {
        static string _UserID = string.Empty;
        static string _UserName = string.Empty;
        static string _Token = string.Empty;

        /// <summary>
        /// 当前登录用户编号
        /// </summary>
        public static long UserID => (long)GetClaimByKey("UserID");

        /// <summary>
        /// 当前登录用户姓名
        /// </summary>
        public static string UserName => (string)GetClaimByKey("UserName");
        /// <summary>
        /// 当前登录用户Token
        /// </summary>
        public static string Token
        {
            get
            {
                try
                {
					var token = HttpContextExtension.Current.Request.Headers["Authorization"];
                    if (token.Count > 0)
                    {
                        _Token = token.ToString().Split(" ")[1];
                    }
                }
                catch
                {
                    _Token = string.Empty;
                }
                return _Token;
            }
        }
        /// <summary>
        /// Gets the claim by key.
        /// </summary>
        /// <returns>The claim by key.</returns>
        /// <param name="key">Key</param>
        public static object GetClaimByKey(string key)
        {
            try
            {
                return HttpContextExtension.Current.User.FindFirst(key).Value;
            }
            catch
            {
                return "";
            }
        }

        #region 后台权限判断(是否有权限)
        /// <summary>
        /// 判断用户是否有权限
        /// </summary>
        /// <param name="AuthName">权限名</param>
        /// <returns>true or false</returns>
        public static bool CheckUsecase(string AuthName) => GetUserUsecaseList().Contains(AuthName);
        /// <summary>
        /// 获取当前用户有权限的用例编号列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetUserUsecaseList()
        {
            return RedisHelper.GetString<List<string>>(CacheKeyConst.UsecaseCodeList + "_" + GetClaimByKey("UserID"));
        }
        #endregion
    }
}
