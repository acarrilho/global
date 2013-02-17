using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Global.Data.Providers.DbManagement.Base
{
    public class DbManagementServiceSection : ConfigurationSection
    {
        /// <summary>
        /// Gets the providers.
        /// </summary>
        /// <value>The providers.</value>
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)base["providers"]; }
        }

        /// <summary>
        /// Gets or sets the default provider.
        /// </summary>
        /// <value>The default provider.</value>
        [StringValidator(MinLength = 1)]
        [ConfigurationProperty("defaultProvider", DefaultValue = "SqlDbManagementProvider")]
        public string DefaultProvider
        {
            get { return (string)base["defaultProvider"]; }
            set { base["defaultProvider"] = value; }
        }
    }
}
