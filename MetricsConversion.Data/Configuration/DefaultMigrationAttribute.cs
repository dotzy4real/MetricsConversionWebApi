using System;
using System.Globalization;
using FluentMigrator;

namespace MetricsConversion.Data.Configuration
{
    /// <summary>
    /// Attribute for a migration
    /// </summary>
    public partial class DefaultMigrationAttribute : MigrationAttribute
    {
        private static readonly string[] _dateFormats = { "yyyy-MM-dd HH:mm:ss", "yyyy.MM.dd HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss:fffffff", "yyyy.MM.dd HH:mm:ss:fffffff", "yyyy/MM/dd HH:mm:ss:fffffff" };

        /// <summary>
        /// Initializes a new instance of the NopMigrationAttribute class
        /// </summary>
        /// <param name="dateTime">The migration date time string to convert on version</param>
        public DefaultMigrationAttribute(string dateTime) :
            base(DateTime.ParseExact(dateTime, _dateFormats, CultureInfo.InvariantCulture).Ticks, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NopMigrationAttribute class
        /// </summary>
        /// <param name="dateTime">The migration date time string to convert on version</param>
        /// <param name="description">The migration description</param>
        public DefaultMigrationAttribute(string dateTime, string description) :
            base(DateTime.ParseExact(dateTime, _dateFormats, CultureInfo.InvariantCulture).Ticks, description)
        {
        }
    }
}
