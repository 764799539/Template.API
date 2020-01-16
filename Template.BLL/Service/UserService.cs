using System;
using System.Collections.Generic;
using System.Text;
using Template.NuGet;

namespace Template.BLL
{
    public class UserService : BaseService, IUserService
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string GetTestContent(string text) => text;
    }
}
