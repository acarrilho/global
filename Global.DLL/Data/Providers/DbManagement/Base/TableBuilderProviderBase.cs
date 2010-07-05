using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Data.Providers.DbManagement.Base
{
    public abstract class TableBuilderProviderBase
    {
        // Properties
        public abstract string TableName { get; set; }

        // Methods
        public abstract bool CreateTable(string tableName, object[] columns);
        public abstract bool CreateTable(string tableName, object columns);
        public abstract bool AlterTable(string tableName, object[] columns);
        public abstract bool AlterTable(string tableName, object columns);
        public abstract bool DropTable(string tableName);
    }
}
