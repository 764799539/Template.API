using Microsoft.AspNetCore.Mvc;
using Template.BLL;
using Template.Model;
using Template.NuGet;

namespace Template.API.Controllers
{
    /// <summary>
    /// 系统API
    /// </summary>
    [ApiController]
    [Route("API/[controller]")]
    public class SystemController : BaseController
    {
        #region 权限控制
        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="entity">权限实体</param>
        /// <returns></returns>
        [HttpPost, Route("SaveAuth")]
        public JsonReturn<bool> SaveAuth(Sys_Auth entity)
        {

            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = true };
        }
        #endregion

    }
}
