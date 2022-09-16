using MetricsConversion.Data.Core;
using MetricsConversion.Data.Configuration;
using System;

namespace MetricsConversion.Data
{
    /// <summary>
    /// Represents the data provider manager
    /// </summary>
    public partial class DataProviderManager : IDataProviderManager
    {
        #region Methods

        /// <summary>
        /// Gets data provider by specific type
        /// </summary>
        /// <param name="dataProviderType">Data provider type</param>
        /// <returns></returns>
        public static IDefaultDataProvider GetDataProvider(DataProviderType dataProviderType)
        {
            switch (dataProviderType)
            {
                //case DataProviderType.SqlServer:
                //    return new MsSqlSimpleMarketDataProvider();
                //case DataProviderType.MySql:
                //    return new MySqlSimpleMarketDataProvider();
                case DataProviderType.PostgreSQL:
                    return new PostgreSqlDataProvider();
                default:
                    throw new Exception($"Not supported data provider name: '{dataProviderType}'");
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets data provider
        /// </summary>
        public IDefaultDataProvider DataProvider
        {
            get
            {
                var dataProviderType = Singleton<DataSettings>.Instance.DataProvider;

                return GetDataProvider(dataProviderType);
            }
        }

        #endregion
    }
}
