using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration.Provider;

namespace Global.Data.Providers.DbManagement.Base
{
    public class DbManagementProviderCollection : ProviderCollection
    {
        /// <summary>
        /// Gets the <see cref="T:DbManagementProvider"/> with the specified name.
        /// </summary>
        /// <value></value>
        public new DbManagementProvider this[string name]
        {
            get { return (DbManagementProvider)base[name]; }
        }

        /// <summary>
        /// Adds the specified provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public void Add(DbManagementProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            if (!(provider is DbManagementProvider))
            {
                throw new ArgumentException("Invalid provider type", "provider");
            }
            base.Add(provider);
        }
    }
}
