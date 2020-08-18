using Chloe;
using Chloe.SqlServer;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Template.NuGet
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class DbContextFactory
    {
        public static string WriteConnectionString { get; set; }
        public static string ReadConnectionString { get; set; }
        public static string DbType { get; set; }

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

        public static IDbContext CreateReadContext() => CreateContext(ReadConnectionString, DbType);
        public static IDbContext CreateWriteContext() => CreateContext(WriteConnectionString, DbType);

        public static IDbContext CreateContext(string connString, string dbType) => CreateSqlServerContext(connString, dbType);
        /// <summary>
        /// 创建数据库上下文对象
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns>SqlServer数据库上下文对象</returns>
        private static IDbContext CreateSqlServerContext(string connString,string dbType)
        {
            dbType = "SqlServer";
            switch (dbType)
            {
                case "SqlServer":
                    MsSqlContext context = new MsSqlContext(connString);
                    //设置分页方式为Offset方式
                    context.PagingMode = PagingMode.OFFSET_FETCH;
                    return context;
                case "MySql":
                    return null;
                case "Oracle":
                    return null;
                case "SQLite":
                    return null;
                case "PostgreSQL":
                    return null;
                default:
                    throw new Exception("Config \"DbType\" is illegal");
            }
        }
    }
}