using MetricsConversion.Domain.Common;
using System;


namespace MetricsConversion.Domain.Logging
{
    public partial class LogConfiguration: BaseEntity
    {
        public string  ModelName { get; set; }

        public string Key { get; set; }

        public bool EnableDisplay { get; set; }

        public bool EnableLog { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets Creating User
        /// </summary>
        public int CreatedBy { get; set; }


        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets user updating
        /// </summary>
        public int UpdatedBy { get; set; }

    }
}
