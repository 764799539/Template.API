using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;
        /// <summary>
        /// 用户服务注入
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="commonService"></param>
        public AuthController(IUserService userService, ICommonService commonService)
        {
            _userService = userService;
            _commonService = commonService;
        }

        #region 权限
        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="entity">权限实体</param>
        /// <returns></returns>
        [HttpPost, Route("SaveAuth")]
        [Authorization("Auth_Save")]
        public JsonReturn<bool> SaveAuth(Sys_Auth entity)
        {
            if (entity.ID <= 0)
            {
                if (!_commonService.IsExist<Sys_Auth>(sa => sa.Name == entity.Name && !sa.IsDelete))
                {
                    entity.CreateBy = AuthManager.UserID;
                    entity.CreateDate = DateTime.Now;
                    _commonService.Insert(entity);
                }
            }
            else
            {
                if (!_commonService.IsExist<Sys_Auth>(sa => sa.Name == entity.Name && !sa.IsDelete))
                {
                    _commonService.Update<Sys_Auth>(sa => sa.ID == entity.ID, sa => new Sys_Auth()
                    {
                        UpdateBy = AuthManager.UserID,
                        UpdateDate = DateTime.Now,
                        Type = entity.Type,
                        ParentID = entity.ParentID,
                        Describe = entity.Describe,
                        Name = entity.Name
                    });
                }
            }
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = true };
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("DeleteAuth")]
        [Authorization("Auth_Del")]
        public JsonReturn<bool> DeleteAuth(long ID)
        {
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = _commonService.Update<Sys_Auth>(sa => sa.ID == ID, sa => new Sys_Auth() { IsDelete = true }) == 1, Msg = ""};
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("DeleteAuth")]
        [Authorization("Auth_Search")]
        public JsonReturn<List<Sys_Auth>> GetAuthList() {
            return new JsonReturn<List<Sys_Auth>> { Status = ResultStatus.OK, Data = _commonService.GetList<Sys_Auth>(sa => sa.IsDelete == false), Msg = "" };
        }
        #endregion

        #region 角色
        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="entity">权限实体</param>
        /// <returns></returns>
        [HttpPost, Route("SaveRole")]
        [Authorization("Role_Save")]
        public JsonReturn<bool> SaveRole(Sys_Role entity)
        {
            if (entity.ID <= 0)
            {
                if (!_commonService.IsExist<Sys_Role>(sa => sa.Name == entity.Name && !sa.IsDelete))
                {
                    entity.CreateBy = AuthManager.UserID;
                    entity.CreateDate = DateTime.Now;
                    _commonService.Insert(entity);
                }
            }
            else
            {
                if (!_commonService.IsExist<Sys_Role>(sa => sa.Name == entity.Name && !sa.IsDelete))
                {
                    _commonService.Update<Sys_Role>(sa => sa.ID == entity.ID, sa => new Sys_Role()
                    {
                        UpdateBy = AuthManager.UserID,
                        UpdateDate = DateTime.Now,
                        ParentID = entity.ParentID,
                        Describe = entity.Describe,
                        Name = entity.Name
                    });
                }
            }
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = true };
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("DeleteRole")]
        [Authorization("Role_Del")]
        public JsonReturn<bool> DeleteRole(long ID)
        {
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = _commonService.Update<Sys_Role>(sa => sa.ID == ID, sa => new Sys_Role() { IsDelete = true }) == 1, Msg = "" };
        }


        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("DeleteRole")]
        [Authorization("Role_Search")]
        public JsonReturn<List<Sys_Role>> GetRoleList()
        {
            return new JsonReturn<List<Sys_Role>> { Status = ResultStatus.OK, Data = _commonService.GetList<Sys_Role>(sa => sa.IsDelete == false), Msg = "" };
        }
        #endregion

        #region 组
        /// <summary>
        /// 保存组
        /// </summary>
        /// <param name="entity">组实体</param>
        /// <returns></returns>
        [HttpPost, Route("SaveGroup")]
        [Authorization("Group_Save")]
        public JsonReturn<bool> SaveGroup(Sys_Group entity)
        {
            if (entity.ID <= 0)
            {
                if (!_commonService.IsExist<Sys_Group>(sa => sa.Name == entity.Name && !sa.IsDelete))
                {
                    entity.CreateBy = AuthManager.UserID;
                    entity.CreateDate = DateTime.Now;
                    _commonService.Insert(entity);
                }
            }
            else
            {
                if (!_commonService.IsExist<Sys_Group>(sa => sa.Name == entity.Name && !sa.IsDelete))
                {
                    _commonService.Update<Sys_Group>(sa => sa.ID == entity.ID, sa => new Sys_Group()
                    {
                        UpdateBy = AuthManager.UserID,
                        UpdateDate = DateTime.Now,
                        ParentID = entity.ParentID,
                        Describe = entity.Describe,
                        Name = entity.Name
                    });
                }
            }
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = true };
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("DeleteGroup")]
        [Authorization("Group_Del")]
        public JsonReturn<bool> DeleteGroup(long ID)
        {
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = _commonService.Update<Sys_Group>(sa => sa.ID == ID, sa => new Sys_Group() { IsDelete = true }) == 1, Msg = "" };
        }


        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("DeleteGroup")]
        [Authorization("Group_Search")]
        public JsonReturn<List<Sys_Group>> GetGroupList()
        {
            return new JsonReturn<List<Sys_Group>> { Status = ResultStatus.OK, Data = _commonService.GetList<Sys_Group>(sa => sa.IsDelete == false), Msg = "" };
        }
        #endregion
    }
}
