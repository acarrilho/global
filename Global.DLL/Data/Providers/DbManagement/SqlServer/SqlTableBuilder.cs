using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Data.Providers.DbManagement.SqlServer
{
    public class SqlTableBuilder : SqlTableBuilderBase
    {
        public SqlTableBuilder(string connectionString, string dbServerName, string dbName)
            : base(connectionString, dbServerName, dbName)
        {
        }
    }
}
