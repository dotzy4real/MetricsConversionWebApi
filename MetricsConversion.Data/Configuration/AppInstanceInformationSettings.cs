
namespace MetricsConversion.Data.Configuration
{
    /// <summary>
    /// Store information settings
    /// </summary>
    public class AppInstanceInformationSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether "powered by Simple market" text should be displayed.
        /// </summary>
        public bool HidePoweredBySimpleMarket { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether App Instance is closed
        /// </summary>
        public bool AppInstanceClosed { get; set; }

        /// <summary>
        /// Gets or sets a picture identifier of the logo. If 0, then the default one will be used
        /// </summary>
        public int LogoPictureId { get; set; }

        /// <summary>
        /// Gets or sets a default App theme
        /// </summary>
        public string DefaultAppInstanceTheme { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user are allowed to select a theme
        /// </summary>
        public bool AllowUserToSelectTheme { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether mini profiler should be displayed in public app instance (used for debugging)
        /// </summary>
        public bool DisplayMiniProfilerInPublicAppInstance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether mini profiler should be displayed only for users with access to the admin area
        /// </summary>
        public bool DisplayMiniProfilerForAdminOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we should display warnings about the new EU cookie law
        /// </summary>
        public bool DisplayEuCookieLawWarning { get; set; }

        /// <summary>
        /// Gets or sets a value of Facebook page URL of the site
        /// </summary>
        public string FacebookLink { get; set; }

        /// <summary>
        /// Gets or sets a value of Twitter page URL of the site
        /// </summary>
        public string TwitterLink { get; set; }

        /// <summary>
        /// Gets or sets a value of YouTube channel URL of the site
        /// </summary>
        public string YoutubeLink { get; set; }
    }
}
