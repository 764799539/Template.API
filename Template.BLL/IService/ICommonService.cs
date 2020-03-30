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
        TEntity Get<TEntity>(object key);
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate);
        TEntity GetByWrite<TEntity>(object key);
        TEntity GetByWrite<TEntity>(Expression<Func<TEntity, bool>> predicate);
        int GetCount<TEntity>(Expression<Func<TEntity, bool>> predicate);
        int GetCountByWrite<TEntity>(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetList<TEntity>();
        List<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> predicate);
        int Delete<TEntity>(Expression<Func<TEntity, bool>> predicate);
        int Delete<TEntity>(TEntity entity);
        int Delete<TEntity>(object key);
        TEntity Add<TEntity>(TEntity entity);
        void AddList<TEntity>(List<TEntity> entities);
        int Update<TEntity>(TEntity entity);
        int Update<TEntity>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> content);
    }
}
