using System;
using System.Collections.Generic;
using System.Text;
using Global.Data.Providers.DbManagement.Base;
using System.Configuration.Provider;
using Global.Config;
using System.Collections.Specialized;

namespace Global.Data.Providers.DbManagement.SqlServer
{
    public class SqlDbManagementBase : DbManagementProvider
    {
        private static object syncRoot = new Object();

        private string _applicationName;
        private string _dbServerName;
        private string _dbName;

        private string _connectionString;
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString
        {
            get { return this._connectionString; }
            set { this._connectionString = value; }
        }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDbManagementProvider"/> class.
		///</summary>
        public SqlDbManagementBase()
		{
        }

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        /// <exception cref="T:System.ArgumentNullException">The name of the provider is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"></see> on a provider after the provider has already been initialized.</exception>
        /// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
        public override void Initialize(string name, NameValueCollection config)
        {
            // Verify that config isn't null
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            // Assign the provider a default name if it doesn't have one
            if (String.IsNullOrEmpty(name))
            {
                name = "SqlDbManagement";
            }

            // Add a default "description" attribute to config if the
            // attribute doesn't exist or is empty
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Sql database management provider");
            }

            // Call the base class's Initialize method
            base.Initialize(name, config);

            // Initialize _applicationName
            _applicationName = config["applicationName"];
            if (string.IsNullOrEmpty(_applicationName))
            {
                _applicationName = "/";
            }
            config.Remove("applicationName");

            #region "Initialize DbServerName"
            string dbServerName = config["dbServerName"];
            if (string.IsNullOrEmpty(dbServerName))
            {
                throw new ProviderException("Empty or missing dbServerName");
            }
            this._dbServerName = config["dbServerName"];
            config.Remove("dbServerName");
            #endregion

            #region "Initialize DbName"
            string dbName = config["dbName"];
            if (string.IsNullOrEmpty(dbName))
            {
                throw new ProviderException("Empty or missing dbName");
            }
            this._dbName = config["dbName"];
            config.Remove("dbName");
            #endregion


            #region "Initialize ConnectionStringName"
            string connectionStringName = config["connectionStringName"];
            if (string.IsNullOrEmpty(connectionStringName))
            {
                throw new ProviderException("Empty or missing connectionStringName.");
            }
            else
            {
                if (ConfigHelper.GetConnectionString(connectionStringName) == null)
                {
                    throw new ProviderException("Missing connection string name.");
                }
            }
            this._connectionString = config["connectionStringName"];
            config.Remove("connectionStringName");
            #endregion

            // Throw an exception if unrecognized attributes remain
            if (config.Count > 0)
            {
                string attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr)) throw new ProviderException("Unrecognized attribute: " + attr);
            }
        }

        private SqlTableBuilder innerSqlTableBuilder;
        ///<summary>
        /// This class is the Data Access Logic Component for the <see cref="TableBuilder"/> business entity.
        ///</summary>
        /// <value></value>
        public override TableBuilderProviderBase TableBuilder
        {
            get
            {
                if (innerSqlTableBuilder == null)
                {
                    lock (syncRoot)
                    {
                        if (innerSqlTableBuilder == null)
                        {
                            this.innerSqlTableBuilder = new SqlTableBuilder(_connectionString, _dbServerName, _dbName);
                        }
                    }
                }

                return innerSqlTableBuilder;
            }
        }

        /// <summary>
        /// Gets the current <c cref="SqlTableBuilder"/>.
        /// </summary>
        /// <value></value>
        public SqlTableBuilder SqlTableBuilder
        {
            get { return TableBuilder as SqlTableBuilder; }
        }

        private SqlColumnBuilder innerSqlColumnBuilder;
        ///<summary>
        /// This class is the Data Access Logic Component for the <see cref="TableBuilder"/> business entity.
        ///</summary>
        /// <value></value>
        public override ColumnBuilderProviderBase ColumnBuilder
        {
            get
            {
                if (innerSqlColumnBuilder == null)
                {
                    lock (syncRoot)
                    {
                        if (innerSqlColumnBuilder == null)
                        {
                            this.innerSqlColumnBuilder = new SqlColumnBuilder();
                        }
                    }
                }

                return innerSqlColumnBuilder;
            }
        }

        /// <summary>
        /// Gets the current <c cref="SqlColumnBuilder"/>.
        /// </summary>
        /// <value></value>
        public SqlColumnBuilder SqlColumnBuilder
        {
            get { return ColumnBuilder as SqlColumnBuilder; }
        }

        private SqlQueryBuilder innerSqlQueryBuilder;

        ///<summary>
        /// This class is the Data Access Logic Component for the <see cref="QueryBuilder"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        /// <value></value>
        public override QueryBuilderProviderBase QueryBuilder
        {
            get
            {
                if (innerSqlQueryBuilder == null)
                {
                    lock (syncRoot)
                    {
                        if (innerSqlQueryBuilder == null)
                        {
                            this.innerSqlQueryBuilder = new SqlQueryBuilder(_connectionString);
                        }
                    }
                }
                return innerSqlQueryBuilder;
            }
        }

        /// <summary>
        /// Gets the current <c cref="SqlQueryBuilderProvider"/>.
        /// </summary>
        /// <value></value>
        public SqlQueryBuilder SqlQueryBuilder
        {
            get { return QueryBuilder as SqlQueryBuilder; }
        }
    }
}
