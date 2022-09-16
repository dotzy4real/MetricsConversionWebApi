using MetricsConversion.Data.Core;

namespace MetricsConversion.Data
{
    /// <summary>
    /// Represents a data provider manager
    /// </summary>
    public partial interface IDataProviderManager
    {
        #region Properties

        /// <summary>
        /// Gets data provider
        /// </summary>
        IDefaultDataProvider DataProvider { get; }

        #endregion
    }
}
