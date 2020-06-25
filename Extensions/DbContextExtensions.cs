using System.Linq;
using Nop.Core.Domain.Common;
using Nop.Data;

namespace Majako.Plugin.Common.Extensions
{
    public static class DbContextExtensions
    {
        public static bool TableExists(this IDbContext dbContext, string tableName)
        {
            var sql = $"SELECT TABLE_NAME as Value FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
            var tableNameQueryType = dbContext.QueryFromSql<StringQueryType>(sql).FirstOrDefault();
            return tableNameQueryType?.Value != null;
        }
    }
}