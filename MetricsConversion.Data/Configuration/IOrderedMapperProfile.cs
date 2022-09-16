using System;
using System.Collections.Generic;
using System.Text;

namespace MetricsConversion.Data.Configuration
{
    public interface IOrderedMapperProfile
    {
        /// <summary>
        /// Gets order of this configuration implementation
        /// </summary>
        int Order { get; }
    }
}
