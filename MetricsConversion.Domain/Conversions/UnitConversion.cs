using MetricsConversion.Domain.Common;
using System;
using System.Net;


namespace MetricsConversion.Domain.Conversions
{
    public partial class UnitConversion: BaseEntity
    {
        public string FromMetric { get; set; }
        public string ToMetric { get; set; }
        public string MetricKey { get; set; }
        public decimal Rate { get; set; }
        public decimal SecondRate { get; set; }
        public string RateOperator { get; set; }
    }

    public class UnitConversionApiRequestData
    {
        public string FromMetric { get; set; }
        public string ToMetric { get; set; }
        public string MetricKey { get; set; }
        public decimal FromMetricValue { get; set; }
    }
    public class UnitConversionResponseData
    {
        public object data { get; set; }
        public string message { get; set; }
        public decimal ConversionValue { get; set; }
        public HttpStatusCode statusCode { get; set; }
    }
}
