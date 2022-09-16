using MetricsConversion.Domain.Conversions;
using System;
using System.Collections.Generic;
using System.Linq;
using MetricsConversion.Data;
using System.Text;

namespace MetricsConversion.Services.Conversions
{
    public class UnitConversionService : IUnitConversionService
    {
        #region Fields
        private readonly IRepository<UnitConversion> _unitConversionRepository;

        #endregion

        #region Ctor
        public UnitConversionService(IRepository<UnitConversion> UnitConversionRepository)
        {
            _unitConversionRepository = UnitConversionRepository;
        }
        #endregion

        #region Methods
        public void DeleteUnitConversion(UnitConversion action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            _unitConversionRepository.Delete(action);
        }

        public IList<UnitConversion> GetAllUnitConversions()
        {
            var query = _unitConversionRepository.Table;
            return query.ToList();
        }

        public UnitConversion GetUnitConversionById(int unitConversionId)
        {
            if (unitConversionId == 0)
                return null;

            return _unitConversionRepository.Table.FirstOrDefault(x => x.Id == unitConversionId);
        }

        public UnitConversion GetUnitConversionByMetricKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            return _unitConversionRepository.Table.FirstOrDefault(x => x.MetricKey == key);
        }
               
        public void InsertUnitConversion(UnitConversion action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            _unitConversionRepository.Insert(action);
        }

        public void UpdateUnitConversion(UnitConversion action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            _unitConversionRepository.Update(action);
        }

        #endregion
    }
}
