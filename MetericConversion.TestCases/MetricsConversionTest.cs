using System;
using MetricsConversion.WebApi.Controllers;
using MetricsConversion.Services.Conversions;
using MetricsConversion.Services.Utility;
using MetricsConversion.Domain.Conversions;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Moq;

namespace MetericConversion.TestCases
{
    public class MetricsConversionTest : IClassFixture<MetricConvertionController>
    {
        MetricConvertionController _controller;
        private readonly IUnitConversionService _unitConversionService;
        public MetricsConversionTest(MetricConvertionController controller, IUnitConversionService unitConversionService)
        {
            _unitConversionService = unitConversionService;
            _controller = new MetricConvertionController(_unitConversionService);
            //_controller = new MetricConvertionController(_unitConversionService);
        }

        [Fact]
        public void CelciusToFarenheit()
        {
            var request = new UnitConversionApiRequestData()
            {
                ToMetric = "Farenheit",
                FromMetric = "Celcius",
                MetricKey = "FahrCels",
                FromMetricValue = 96
            };

            var response = _controller.ProcessUnitConversion(request);


            Assert.NotNull(response);
            Assert.IsType<Task<IActionResult>>(response);
            Assert.True(response.IsCompletedSuccessfully);


            var resultObject = (UnitConversionResponseData)response.Result;

            Assert.IsType<decimal>(resultObject.ConversionValue);

            Assert.Equal(35.56, Convert.ToDouble(resultObject.ConversionValue), 2);
        }
    }
}
