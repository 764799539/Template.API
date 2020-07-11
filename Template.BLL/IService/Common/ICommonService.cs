using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Template.Model;
using Template.NuGet;


namespace Template.BLL
{
    public interface ICommonService : IBaseService
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>实体对象</returns>
        TEntity Insert<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="entities">实体对象列表</param>
        void BulkInsert<TEntity>(List<TEntity> entities) where TEntity : BaseEntity;

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>受影响行数</returns>
        int Delete<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity;

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>受影响行数</returns>
        int Delete<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Key">主键值</param>
        /// <returns>受影响行数</returns>
        int Delete<TEntity>(object Key) where TEntity : BaseEntity;

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>受影响行数</returns>
        int Update<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// 指定字段、条件更新实体
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <param name="Content">更新内容</param>
        /// <returns>受影响行数</returns>
        int Update<TEntity>(Expression<Func<TEntity, bool>> Predicate, Expression<Func<TEntity, TEntity>> content) where TEntity : BaseEntity;

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Key">主键值</param>0
        /// <returns>实体对象</returns>
        TEntity Get<TEntity>(object Key) where TEntity : BaseEntity;

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>实体对象</returns>
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity;

        /// <summary>
        /// 通过写库获取实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Key">主键值</param>
        /// <returns>实体对象</returns>
        TEntity GetByWrite<TEntity>(object Key) where TEntity : BaseEntity;

        /// <summary>
        /// 通过写库获取实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>实体对象</returns>
        TEntity GetByWrite<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity;

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>数量</returns>
        int GetCount<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity;

        /// <summary>
        /// 通过写库获取数量
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>数量</returns>
        int GetCountByWrite<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity;

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>是否存在</returns>
        bool IsExist<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity;

        /// <summary>
        /// 通过写库判断是否存在
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>是否存在</returns>
        bool IsExistByWrite<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity;

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <returns>实体对象列表</returns>
        List<TEntity> GetList<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>实体对象列表</returns>
        List<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity;

        ///// <summary>
        ///// 软删除
        ///// </summary>
        ///// <typeparam name="TEntity">实体类</typeparam>
        ///// <param name="ID">主键值</param>
        ///// <returns></returns>
        //public int SoftDelete<TEntity>(long ID) where TEntity : BaseEntity;


        ///// <summary>
        ///// 指定字段插入
        ///// </summary>
        ///// <typeparam name="TEntity">实体类</typeparam>
        ///// <param name="entity">实体对象</param>
        ///// <returns>实体对象</returns>
        //long Insert<TEntity>(Expression<Func<TEntity>> Predicate) where TEntity : BaseEntity;

    }
}
