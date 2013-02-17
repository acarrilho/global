using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Global.Data.Providers.DbManagement.Base
{
    public abstract class QueryBuilderProviderBase
    {
        public abstract DataSet SelectAll(string tableName);

        public abstract DataSet SelectByID(string tableName, object iD);

        public abstract DataSet SelectWithWhereClause(string tableName,
            string whereClause);
    }
}
