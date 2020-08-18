using System;
using System.Collections.Generic;
using System.Text;
using Template.NuGet;
using Template.Model;

namespace Template.BLL
{
    public class UserService : BaseService, IUserService
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Sys_User GetTestContent(string text) {
            Sys_User result = ReadDbContext.Query<Sys_User>().FirstOrDefault();
            return result;
        }
    }
}
