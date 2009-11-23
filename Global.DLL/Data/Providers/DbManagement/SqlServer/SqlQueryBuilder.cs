using System;
using System.Collections.Generic;
using System.Text;
using Global.Data.Providers.DbManagement.Base;

namespace Global.Data.Providers.DbManagement.SqlServer
{
    public class SqlQueryBuilder : SqlQueryBuilderBase
    {
        public SqlQueryBuilder(string connectionString)
            : base(connectionString)
        {
        }
    }
}
