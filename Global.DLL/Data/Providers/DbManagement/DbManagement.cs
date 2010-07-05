using System.Web.Configuration;
using System.Configuration.Provider;
using Global.Data.Providers.DbManagement.Base;

namespace Global.Data.Providers.Dbmanagement
{
    ///<summary>
    ///</summary>
    public class DbManagement
    {
        private static volatile DbManagementProvider _provider;
        private static volatile DbManagementProviderCollection _providers;
        private static volatile DbManagementServiceSection _section;

        private static readonly object SyncRoot = new object();

        private DbManagement()
        {
        }

        ///<summary>
        /// Configuration based provider loading, will load the providers on first call.
        ///</summary>
        private static void LoadProviders()
        {
            // Avoid claiming lock if providers are already loaded
            if (_provider == null)
            {
                lock (SyncRoot)
                {
                    // Do this again to make sure _provider is still null
                    if (_provider == null)
                    {
                        // Loads the db management config section
                        LoadDbManagementSection();

                        // Load registered providers and point _provider to the default provider
                        _providers = new DbManagementProviderCollection();

                        ProvidersHelper.InstantiateProviders(_section.Providers, _providers, typeof(DbManagementProvider));
                        _provider = _providers[_section.DefaultProvider];

                        if (_provider == null)
                        {
                            throw new ProviderException("Unable to load default DbManagementProvider");
                        }
                    }
                }
            }
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        ///<exception cref="ProviderException"></exception>
        public static DbManagementServiceSection LoadDbManagementSection()
        {
            // Try to get a reference to the default <dbManagementService> section
            _section = WebConfigurationManager.GetSection("dbManagementService") as DbManagementServiceSection ??
                       WebConfigurationManager.GetSection("Global.Data") as DbManagementServiceSection;

            if (_section == null)
            {
                throw new ProviderException("Unable to load DbManagementServiceSection");
            }

            return _section;
        }

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="TableBuilder"/> business entity.
        ///</summary>
        public static TableBuilderProviderBase TableBuilder
        {
            get
            {
                LoadProviders();
                return _provider.TableBuilder;
            }
        }

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="ColumnBuilder"/> business entity.
        ///</summary>
        public static ColumnBuilderProviderBase ColumnBuilder
        {
            get
            {
                LoadProviders();
                return _provider.ColumnBuilder;
            }
        }

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="QueryBuilder"/> business entity.
        ///</summary>
        public static QueryBuilderProviderBase QueryBuilder
        {
            get
            {
                LoadProviders();
                return _provider.QueryBuilder;
            }
        }
    }
}
