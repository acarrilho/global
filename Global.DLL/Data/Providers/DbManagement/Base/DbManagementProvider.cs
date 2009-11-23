using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration.Provider;
using System.Collections.Specialized;

namespace Global.Data.Providers.DbManagement.Base
{
    public class DbManagementProvider : ProviderBase
    {
        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
        }

        ///<summary>
        /// Current TableBuilderBase instance.
        ///</summary>
        public virtual TableBuilderProviderBase TableBuilder { get { throw new NotImplementedException(); } }

        ///<summary>
        /// Current ColumnBuilderProviderBase instance.
        ///</summary>
        public virtual ColumnBuilderProviderBase ColumnBuilder { get { throw new NotImplementedException(); } }

        ///<summary>
        /// Current QueryBuilderProviderBase instance.
        ///</summary>
        public virtual QueryBuilderProviderBase QueryBuilder { get { throw new NotImplementedException(); } }
    }
}
