using System;
using System.Collections.Generic;
using System.Text;
using MetricsConversion.Domain.Conversions;

namespace MetricsConversion.Services.Conversions
{
    public interface IUnitConversionService
    {
        /// <summary>
        /// Deletes an Action
        /// </summary>
        /// <param name="UnitConversion">UnitConversion</param>
        void DeleteUnitConversion(UnitConversion unitConversion);

        /// <summary>
        /// Gets all Action
        /// </summary>
        /// <returns>UnitConversion</returns>
        IList<UnitConversion> GetAllUnitConversions();

        /// <summary>
        /// Gets an Action
        /// </summary>
        /// <param name="UnitConversionId">UnitConversion identifier</param>
        /// <returns>UnitConversion</returns>
        UnitConversion GetUnitConversionById(int unitConversionId);


        UnitConversion GetUnitConversionByMetricKey(string key);

        /// <summary>
        /// Inserts an Action
        /// </summary>
        /// <param name="UnitConversion">UnitConversion</param>
        void InsertUnitConversion(UnitConversion unitConversion);

        /// <summary>
        /// Updates an Action
        /// </summary>
        /// <param name="UnitConversion">UnitConversion</param>
        void UpdateUnitConversion(UnitConversion unitConversion);

    }
}
