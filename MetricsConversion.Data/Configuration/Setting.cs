using MetricsConversion.Domain.Common;

namespace MetricsConversion.Data.Configuration
{
    /// <summary>
    /// Represents a setting
    /// </summary>
    public partial class Setting : BaseEntity
    {

        public Setting() { }

        public Setting(string name, string value, int appInstanceId = 0)
        {
            this.Name = name;
            this.Value = value;
            this.AppInstanceId = appInstanceId;
        }

        /// <summary>
        /// Gets or set settings identifier
        /// </summary>
       // public int SettingID { get; set; }


        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the App Instance for which this setting is valid. 0 is set when the setting is for all schools
        /// </summary>
        public int AppInstanceId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
