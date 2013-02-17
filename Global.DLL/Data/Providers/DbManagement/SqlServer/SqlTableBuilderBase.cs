using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Global.Data.Providers.DbManagement.Base;

namespace Global.Data.Providers.DbManagement.SqlServer
{
    public class SqlTableBuilderBase : TableBuilderProviderBase
    {
        private string _connectionString;
        private string _dbServerName;
        private string _dbName;

        private string _tableName;
        public override string TableName 
        { 
            get 
            { 
                return _tableName; 
            } 
            set 
            { 
                _tableName = value; 
            } 
        }

        public SqlTableBuilderBase(string connectionString,
            string dbServerName, string dbName)
        {
            this._connectionString = connectionString;
            this._dbServerName = dbServerName;
            this._dbName = dbName;
        }

        public override bool CreateTable(string tableName, object[] columns)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    //Server server = new Server(new ServerConnection(connection));
                    //Database db = server.Databases[_dbName];
                    //Table table = new Table(db, tableName);
                    //Column column = new Column(table, "Column1", DataType.Int);
                    //table.Columns.Add(column);

                    //table.Create();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override bool CreateTable(string tableName, object columns)
        {
            try
            {
                //Table table = null;

                //using (SqlConnection connection = new SqlConnection(_connectionString))
                //{
                //    Server server = new Server(new ServerConnection(connection));
                //    Database db = server.Databases[_dbName];
                //    table = new Table(db, tableName);
                //    List<Column> coll = (List<Column>)columns;
                //    foreach (Column obj in coll)
                //    {
                //        if (obj.IsPrimaryKey)
                //        {
                //            Column column = new Column(table, obj.Name, (DataType)obj.DataType);
                //            Index index = new Index(table, "PK_" + tableName + "_" + obj.Name);
                //            index.IndexedColumns.Add(new IndexedColumn(index, obj.Name));
                //            index.IndexKeyType = IndexKeyType.DriPrimaryKey;

                //            table.Indexes.Add(index);
                //            table.Columns.Add(column);
                //        }
                //        else if (obj.IsForeignKey)
                //        {
                //            Column column = new Column(table, obj.Name, (DataType)obj.DataType);
                //            table.Columns.Add(column);

                //            ForeignKey foreignKey = new ForeignKey(table, obj.ForeignKeyName); // + obj.ForeignKeyColumnName);
                //            foreignKey.ReferencedTable = obj.ForeignKeyTableName;
                //            ForeignKeyColumn fkColumn = new ForeignKeyColumn(foreignKey,
                //                obj.Name, obj.ForeignKeyColumnName);
                //            foreignKey.Columns.Add(fkColumn);
                //            table.ForeignKeys.Add(foreignKey);
                //        }
                //        else
                //        {
                //            Column column = new Column(table, obj.Name, (DataType)obj.DataType);
                //            column.Nullable = obj.AllowNulls;
                //            if (obj.IsUnique)
                //            {
                //                Index index = new Index(table, String.Format("UNIQUE_{0}_{1}",tableName, obj.Name));
                //                index.IndexedColumns.Add(new IndexedColumn(index, obj.Name));
                //                index.IndexKeyType = IndexKeyType.DriUniqueKey;
                //                table.Indexes.Add(index);
                //            }
                //            table.Columns.Add(column);
                //        }
                //    }
                //}

                //if (table != null)
                //{
                //    table.Create();
                //}

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override bool AlterTable(string tableName, object[] columns)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool AlterTable(string tableName, object columns)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool DropTable(string tableName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    Server server = new Server(new ServerConnection(connection));
                    Database db = server.Databases[_dbName];
                    Table table = db.Tables[tableName];
                    if(table != null) table.Drop();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
