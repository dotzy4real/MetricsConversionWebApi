using MetricsConversion.WebApi.Controllers;
using MetricsConversion.Services.Conversions;
using MetricsConversion.Services.Utility;
using MetricsConversion.Domain.Conversions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MetricsConversion.Test
{
    //[TestClass]
    //public class MetricsConversionTest
    //{
    //    MetricConvertionController _controller;
    //    IUnitConversionService _unitConversionService;

    //    public MetricsConversionTest(IUnitConversionService unitConversionService)
    //    {
    //        _unitConversionService = unitConversionService;
    //        _controller = new MetricConvertionController(_unitConversionService);

    //    }

    //    [DataTestMethod]
    //    public void CelciusToFarenheit()
    //    {
    //        var request = new UnitConversionApiRequestData()
    //        {
    //            ToMetric = "Farenheit",
    //            FromMetric = "Celcius",
    //            MetricKey = "FahrCels",
    //            FromMetricValue = 96
    //        };

    //        var response = UnitConversionProcessor.ProcessTheUnitConversion(request, _unitConversionService);

    //        Assert.IsInstanceOfType(response, typeof(UnitConversionResponseData));

    //        Assert.IsInstanceOfType(response.ConversionValue, typeof(decimal));

    //        Assert.AreEqual(35.56, response.ConversionValue);
    //    }
    //}
}
