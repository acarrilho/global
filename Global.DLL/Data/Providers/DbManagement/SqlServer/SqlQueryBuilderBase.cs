using System;
using System.Collections.Generic;
using System.Text;
using Global.Data.Providers.DbManagement.Base;

namespace Global.Data.Providers.DbManagement.SqlServer
{
    public class SqlQueryBuilderBase : QueryBuilderProviderBase
    {
        private string _connectionString;

        public SqlQueryBuilderBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override System.Data.DataSet SelectAll(string tableName)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataSet SelectByID(string tableName, object iD)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataSet SelectWithWhereClause(string tableName, string whereClause)
        {
            throw new NotImplementedException();
        }
    }
}
