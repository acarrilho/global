using System;
using System.Collections.Generic;
using System.Text;
using Global.Data.Providers.DbManagement.Base;
using Microsoft.SqlServer.Management.Smo;

namespace Global.Data.Providers.DbManagement.SqlServer
{
    public class SqlColumnBuilderBase : ColumnBuilderProviderBase
    {
        private string _connectionString;
        public string ConnectionString { get { return _connectionString; } set { _connectionString = value; } }

        private string _dbServerInstanceName;
        public string DbServerInstanceName { get { return _dbServerInstanceName; } set { _dbServerInstanceName = value; } }

        private string _dbName;
        public string DbName { get { return _dbName; } set { _dbName = value; } }

        public SqlColumnBuilderBase()
        {
        }

        public SqlColumnBuilderBase(string connectionString, string dbServerInstanceName, string dbName)
        {
            this._connectionString = connectionString;
            this._dbServerInstanceName = dbServerInstanceName;
            this._dbName = dbName;
        }

        private void ValidateRequiredFields()
        {
            if (String.IsNullOrEmpty(_connectionString))
                throw new Exception("ConnectionString is a required field.");

            if (String.IsNullOrEmpty(_dbServerInstanceName))
                throw new Exception("DbServerInstanceName is a required field.");

            if (String.IsNullOrEmpty(_dbName))
                throw new Exception("DbName is a required field.");
        }

        public override object CreateColumn(string name, object dataType, bool allowNulls)
        {
            throw new NotImplementedException();
        }

        public override object CreateUniqueColumn(string name, object dataType, bool allowNulls)
        {
            throw new NotImplementedException();
        }

        public override object CreatePrimaryKeyColumn(string name, object dataType)
        {
            throw new NotImplementedException();
        }

        public override object CreateForeignKeyColumn(string name, object dataType, string foreignTableName, string foreignColumnName)
        {
            throw new NotImplementedException();
        }

        public override object CreateForeignKeyColumn(string name, object dataType, string foreignTableName, string foreignColumnName, string foreignKeyName)
        {
            throw new NotImplementedException();
        }
    }
}
