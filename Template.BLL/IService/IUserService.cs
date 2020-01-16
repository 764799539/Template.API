using System;
using System.Collections.Generic;
using System.Text;
using Template.NuGet;

namespace Template.BLL
{
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// 获取测试文本
        /// </summary>
        /// <param name="text">测试文本</param>
        /// <returns>测试文本</returns>
        string GetTestContent(string text);
    }
}
