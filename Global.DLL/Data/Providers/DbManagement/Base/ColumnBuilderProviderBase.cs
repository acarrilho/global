using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Data.Providers.DbManagement.Base
{
    public abstract class ColumnBuilderProviderBase
    {
        public abstract object CreateColumn(string name, object dataType, bool allowNulls);

        //public abstract object CreateColumn(string name, object dataType,
        //    bool allowNulls, string definition);

        //public abstract object CreateUniqueColumn(string name, object dataType,
        //    bool allowNulls, string definition);

        public abstract object CreateUniqueColumn(string name, object dataType,
            bool allowNulls);

        public abstract object CreatePrimaryKeyColumn(string name, object dataType);

        public abstract object CreateForeignKeyColumn(string name, object dataType,
            string foreignTableName, string foreignColumnName);

        public abstract object CreateForeignKeyColumn(string name, object dataType,
            string foreignTableName, string foreignColumnName, string foreignKeyName);
    }
}
