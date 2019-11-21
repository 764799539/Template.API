using Chloe;
using Chloe.SqlServer;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Teamplate.NuGet
{
    public class DbContextFactory
    {
        // Methods
        static DbContextFactory()
        {
            WriteConnectionString = ConfigHelper.GetAppConfig("DB:WriteConnString");
            ReadConnectionString = ConfigHelper.GetAppConfig("DB:ReadConnString");
            string appConfig = ConfigHelper.GetAppConfig("DB:DbType");
            if (!string.IsNullOrEmpty(appConfig))
            {
                DbType = appConfig.ToLower();
            }
        }

        public static IDbContext CreateContext(string connString) =>
            CreateSqlServerContext(connString);

        public static IDbContext CreateReadContext() =>
            CreateContext(ReadConnectionString);

        private static IDbContext CreateSqlServerContext(string connString)
        {
            MsSqlContext context = new MsSqlContext(connString);
            context.PagingMode = PagingMode.OFFSET_FETCH;
            return context;
        }

        public static IDbContext CreateWriteContext() =>
            CreateContext(WriteConnectionString);

        public static string WriteConnectionString { get; set; }

        public static string ReadConnectionString { get; set; }

        public static string DbType { get; set; }
    }

}
