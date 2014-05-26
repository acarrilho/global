using System;
using System.Configuration;

namespace Global.Config
{
    ///<summary>
    /// Performs current actions with the configuration files.
    ///</summary>
    [Obsolete("Methods were renamed and moved to Common.")]
    public class ConfigHelper
    {
        /// <summary>
        /// Gets a specified setting in the AppSettings section.
        /// </summary>
        /// <param name="key">A name identifying the setting value.</param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Gets a specified setting in the AppSettings section.
        /// </summary>
        /// <param name="index">The index of the setting value.</param>
        /// <returns></returns>
        public static string GetAppSetting(int index)
        {
            return ConfigurationManager.AppSettings[index];
        }

        /// <summary>
        /// Gets a specified connection string in the ConnectionsStrings section.
        /// </summary>
        /// <param name="name">A name identifying the connection string value.</param>
        /// <returns></returns>
        public static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /// <summary>
        /// Gets a specified connection string in the ConnectionsStrings section.
        /// </summary>
        /// <param name="index">The index of the setting value.</param>
        /// <returns></returns>
        public static string GetConnectionString(int index)
        {
            return ConfigurationManager.ConnectionStrings[index].ConnectionString;
        }

        ///<summary>
        /// Loads a specified configuration section.
        ///</summary>
        ///<param name="sectionName">The configuration section name.</param>
        ///<typeparam name="TReturn">The configuration section object class.</typeparam>
        ///<returns>The configuration section object.</returns>
        public static TReturn LoadConfigurationSection<TReturn>(string sectionName)
        {
            return (TReturn)ConfigurationManager.GetSection(sectionName);
        }
    }
}
