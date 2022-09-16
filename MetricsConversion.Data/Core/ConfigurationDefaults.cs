using MetricsConversion.Data.Caching;

namespace MetricsConversion.Data.Core
{
    /// <summary>
    /// Represents default values related to configuration services
    /// </summary>
    public static partial class ConfigurationDefaults
    {
        #region Caching defaults

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static CacheKey SettingsAllAsDictionaryCacheKey => new CacheKey("SimpleMarket.setting.all.as.dictionary", SettingsPrefixCacheKey);

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static CacheKey SettingsAllCacheKey => new CacheKey("SimpleMarket.setting.all", SettingsPrefixCacheKey);

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        public static string SettingsPrefixCacheKey => "SimpleMarket.setting.";

        #endregion
    }
}