using Chloe;
using System;
using System.Threading.Tasks;

namespace Template.NuGet
{
    public abstract class BaseService : IServiceFactoryProvider, IDisposable
    {
        private IDbContext _WriteDbContext;
        private IDbContext _ReadDbContext;

        /// <summary>
        /// 写库上下文
        /// </summary>
        public IDbContext WriteDbContext
        {
            get => DbContextFactory.CreateWriteContext();
            set => _WriteDbContext = value;
        }

        /// <summary>
        /// 读库上下文
        /// </summary>
        public IDbContext ReadDbContext
        {
            get => DbContextFactory.CreateReadContext();
            set => _ReadDbContext = value;
        }

        public IServiceFactory ServiceFactory { get; set; }

        protected BaseService() : this(null)
        {
        }

        protected BaseService(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }

        public void Dispose()
        {
            if (_WriteDbContext != null)
                _WriteDbContext.Dispose();
            if (_ReadDbContext != null)
                _ReadDbContext.Dispose();
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }

    //DbContextExtension.DoWithTransaction方法弃用
    //public Task DoAsync(Action<IDbContext> act, bool? startTransaction = new bool?()) =>
    //    Task.Run(delegate {
    //        using (IDbContext dbContext = DbContextFactory.CreateWriteContext())
    //        {
    //            if (startTransaction.HasValue && startTransaction.Value)
    //            {
    //                DbContextExtension.DoWithTransaction(dbContext, delegate
    //                {
    //                    act(dbContext);
    //                });
    //            }
    //            else
    //            {
    //                act(dbContext);
    //            }
    //        }
    //    });

}
