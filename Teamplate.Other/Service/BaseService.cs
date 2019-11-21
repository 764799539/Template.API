using Chloe;
using System;
using System.Threading.Tasks;

namespace Teamplate.Other
{
    public abstract class BaseService : IServiceFactoryProvider, IDisposable
    {
        // Fields
        private IDbContext _WriteDbContext;
        private IDbContext _ReadDbContext;

        // Methods
        protected BaseService() : this(null)
        {
        }

        protected BaseService(IServiceFactory serviceFactory)
        {
            this.ServiceFactory = serviceFactory;
        }

        public void Dispose()
        {
            if (this._WriteDbContext != null)
            {
                this._WriteDbContext.Dispose();
            }
            if (this._ReadDbContext != null)
            {
                this._ReadDbContext.Dispose();
            }
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public Task DoAsync(Action<IDbContext> act, bool? startTransaction = new bool?()) =>
            Task.Run(delegate {
                using (IDbContext dbContext = DbContextFactory.CreateWriteContext())
                {
                    if (startTransaction.HasValue && startTransaction.Value)
                    {
                        DbContextExtension.DoWithTransaction(dbContext, delegate {
                            act(dbContext);
                        });
                    }
                    else
                    {
                        act(dbContext);
                    }
                }
            });

        // Properties
        public IServiceFactory ServiceFactory { get; set; }

        public IDbContext WriteDbContext
        {
            get =>
                DbContextFactory.CreateWriteContext();
            set =>
                this._WriteDbContext = value;
        }

        public IDbContext ReadDbContext
        {
            get =>
                DbContextFactory.CreateReadContext();
            set =>
                this._ReadDbContext = value;
        }
    }

}
