
namespace MetricsConversion.Data.Core
{
    /// <summary>
    /// AppInstance context
    /// </summary>
    public interface IAppInstanceContext
    {
        /// <summary>
        /// Gets the current store
        /// </summary>
        AppInstance CurrentAppInstance { get; }

        /// <summary>
        /// Gets active App Instance scope configuration
        /// </summary>
        int ActiveAppInstanceScopeConfiguration { get; }
    }
}
