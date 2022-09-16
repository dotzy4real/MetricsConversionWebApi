using Microsoft.AspNetCore.Mvc;
using MetricsConversion.Domain.Conversions;
using MetricsConversion.Services.Conversions;
using System;
using System.Threading.Tasks;
using MetricsConversion.Services.Utility;
using System.Net;

namespace MetricsConversion.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricConvertionController : Controller
    {
        #region Fields

        private readonly IUnitConversionService _unitConversionService;

        #endregion


        #region Ctor
        public MetricConvertionController(IUnitConversionService unitConversionService)
        {
            _unitConversionService = unitConversionService;
        }
        
        #endregion


        #region Methods

        [HttpGet]
        public string Index()
        {
            return "Welcome to our Metrics Conversion Web API";
        }

        [HttpPost("ProcessUnitConversion")]
        public async Task<IActionResult> ProcessUnitConversion(UnitConversionApiRequestData request)
        {
            var response = new UnitConversionResponseData();
            try
            {
                response = await UnitConversionProcessor.ProcessUnitConversion(request, _unitConversionService);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = ex.InnerException + ex.Message;
                response.statusCode = HttpStatusCode.InternalServerError;
                return BadRequest(response);
            }
        }
        #endregion
    }
}
