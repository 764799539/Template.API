using System;
using System.Collections.Generic;
using System.Text;
using Template.NuGet;
using Template.Model;
using System.Linq.Expressions;
using Chloe.SqlServer;
using Chloe;

namespace Template.BLL
{
    public class CommonService : BaseService, ICommonService
    {
        public TEntity Add<TEntity>(TEntity entity)
        {
            return WriteDbContext.Insert(entity);
        }

        public void AddList<TEntity>(List<TEntity> entities)
        {
            WriteDbContext.BulkInsert(entities);
        }

        public int Delete<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            return WriteDbContext.Delete(predicate);
        }

        public int Delete<TEntity>(TEntity entity)
        {
            return WriteDbContext.Delete(entity);
        }

        public int Delete<TEntity>(object key)
        {
            return WriteDbContext.Delete(key);
        }

        public TEntity Get<TEntity>(object key)
        {
            //return ReadDbContext.Query<TEntity>(key).FirstOrDefault();
            throw new NotImplementedException();
        }

        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            return ReadDbContext.Query<TEntity>(predicate).FirstOrDefault();
        }

        public TEntity GetByWrite<TEntity>(object key)
        {
            //return WriteDbContext.Query<TEntity>(key).FirstOrDefault();
            throw new NotImplementedException();
        }

        public TEntity GetByWrite<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            return WriteDbContext.Query<TEntity>(predicate).FirstOrDefault();
        }

        public int GetCount<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            return ReadDbContext.Query<TEntity>(predicate).Count();
        }

        public int GetCountByWrite<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            return WriteDbContext.Query<TEntity>(predicate).Count();
        }

        public List<TEntity> GetList<TEntity>()
        {
            return ReadDbContext.Query<TEntity>().ToList<TEntity>();
        }

        public List<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            return ReadDbContext.Query<TEntity>(predicate).ToList<TEntity>();
        }

        public int Update<TEntity>(TEntity entity)
        {
            return WriteDbContext.Update<TEntity>(entity);
        }

        public int Update<TEntity>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> content)
        {
            return WriteDbContext.Update<TEntity>(predicate, content);
        }
    }
}
