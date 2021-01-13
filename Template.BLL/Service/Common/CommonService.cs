using Chloe;
using Chloe.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Template.Model;
using Template.NuGet;

namespace Template.BLL
{
    public class CommonService : BaseService, ICommonService
    {
        IServiceFactory IBaseService.ServiceFactory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>实体对象</returns>
        public async Task<TEntity> Insert<TEntity>(TEntity entity) where TEntity : BaseEntity => await WriteDbContext.InsertAsync(entity);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="entities">实体对象列表</param>
        public void BulkInsert<TEntity>(List<TEntity> entities) where TEntity : BaseEntity => WriteDbContext.BulkInsert(entities);

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>受影响行数</returns>
        public async Task<int> Delete<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity => await WriteDbContext.DeleteAsync(Predicate);

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>受影响行数</returns>
        public async Task<int> Delete<TEntity>(TEntity entity) where TEntity : BaseEntity => await WriteDbContext.DeleteAsync(entity);

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Key">主键值</param>
        /// <returns>受影响行数</returns>
        public async Task<int> Delete<TEntity>(object Key) where TEntity : BaseEntity => await WriteDbContext.DeleteAsync(Key);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>受影响行数</returns>
        public async Task<int> Update<TEntity>(TEntity entity) where TEntity : BaseEntity => await WriteDbContext.UpdateAsync(entity);
        /// <summary>
        /// 指定字段、条件更新实体
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <param name="Content">更新内容</param>
        /// <returns>受影响行数</returns>
        public async Task<int> Update<TEntity>(Expression<Func<TEntity, bool>> Predicate, Expression<Func<TEntity, TEntity>> Content) where TEntity : BaseEntity => await WriteDbContext.UpdateAsync(Predicate, Content);

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Key">主键值</param>0
        /// <returns>实体对象</returns>
        public async Task<TEntity> Get<TEntity>(object Key) where TEntity : BaseEntity => await ReadDbContext.Query<TEntity>(Key.ToString()).FirstOrDefaultAsync();

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>实体对象</returns>
        public async Task<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity => await ReadDbContext.Query(Predicate).FirstOrDefaultAsync();

        /// <summary>
        /// 通过写库获取实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Key">主键值</param>
        /// <returns>实体对象</returns>
        public async Task<TEntity> GetByWrite<TEntity>(object Key) where TEntity : BaseEntity => await WriteDbContext.Query<TEntity>(Key.ToString()).FirstOrDefaultAsync();

        /// <summary>
        /// 通过写库获取实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>实体对象</returns>
        public async Task<TEntity> GetByWrite<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity => await WriteDbContext.Query(Predicate).FirstOrDefaultAsync();

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>数量</returns>
        public async Task<int> GetCount<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity => await ReadDbContext.Query(Predicate).CountAsync();

        /// <summary>
        /// 通过写库获取数量
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>数量</returns>
        public async Task<int> GetCountByWrite<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity => await WriteDbContext.Query(Predicate).CountAsync();

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>是否存在</returns>
        public async Task<bool> IsExist<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity => await ReadDbContext.Query(Predicate).CountAsync() > 0;

        /// <summary>
        /// 通过写库判断是否存在
        /// </summary>
        /// <typeparam name="TEntity">实体类</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>是否存在</returns>
        public async Task<bool> IsExistByWrite<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity => await WriteDbContext.Query(Predicate).CountAsync() > 0;

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <returns>实体对象列表</returns>
        public async Task<List<TEntity>> GetList<TEntity>() where TEntity : BaseEntity => await ReadDbContext.Query<TEntity>().ToListAsync();

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <param name="Predicate">Lambda条件</param>
        /// <returns>实体对象列表</returns>
        public async Task<List<TEntity>> GetList<TEntity>(Expression<Func<TEntity, bool>> Predicate) where TEntity : BaseEntity => await ReadDbContext.Query(Predicate).ToListAsync();







        ///// <summary>
        ///// 软删除
        ///// </summary>
        ///// <typeparam name="TEntity">实体类</typeparam>
        ///// <param name="ID">主键值</param>
        ///// <returns></returns>
        //public int SoftDelete<TEntity>(long ID) where TEntity : BaseEntity
        //{
        //    return WriteDbContext.Update<TEntity>(sa => sa.ID == ID, sa => new BaseEntity() { IsDelete = true });
        //}

        ///// <summary>
        ///// 指定字段插入
        ///// </summary>
        ///// <typeparam name="TEntity">实体类</typeparam>
        ///// <param name="entity">实体对象</param>
        ///// <returns>实体对象</returns>
        //public long Insert<TEntity>(Expression<Func<TEntity>> Predicate) where TEntity : BaseEntity
        //{
        //    return 0;
        //}

    }
}
