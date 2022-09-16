using System;
using System.Threading.Tasks;
using System.Net;
using MetricsConversion.Services.Core;
using MetricsConversion.Services.Conversions;
using MetricsConversion.Domain.Conversions;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MetricsConversion.Services.Utility
{
    public static class UnitConversionProcessor
    {
        private static LockProvider<int> LockProvider = new LockProvider<int>();

        public static async Task<UnitConversionResponseData> ProcessUnitConversion(UnitConversionApiRequestData request, IUnitConversionService unitConversionService)
        {
            var response = new UnitConversionResponseData();

            // wait for this process to finish execution before executing another process to avoid deadlock
            await LockProvider.WaitAsync(ApiLockData.UnitConversionProcess);
            try
            {
                response = ProcessTheUnitConversion(request, unitConversionService);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = HttpStatusCode.InternalServerError;
            }
            finally
            {
                // release the lock
                LockProvider.Release(ApiLockData.UnitConversionProcess);
            }
            return response;
        }

        public static UnitConversionResponseData ProcessTheUnitConversion(UnitConversionApiRequestData request, IUnitConversionService unitConversionService)
        {
            var response = new UnitConversionResponseData();
            var unitConversion = unitConversionService.GetUnitConversionByMetricKey(request.MetricKey);

            // conversion does not exist in db return bad request
            if (unitConversion == null)
            {
                return UnitConversionBadRequest("No Conversion Rate Available for Provided Metric Key");
            }

            // your metrics fields to or from does not match with that from the db return bad request
            if (unitConversion.FromMetric != request.FromMetric || unitConversion.ToMetric != request.ToMetric)
            {
                return UnitConversionBadRequest("Incorrect Data Provided for Either FromMetric or ToMetric Fields");
            }

            (string msg, decimal val) = ProcessConversionLogic(unitConversion, request);
            response.message = msg;
            response.ConversionValue = val;
            response.statusCode = HttpStatusCode.OK;
            return response;
        }

        public static (string, decimal) ProcessConversionLogic(UnitConversion conversion, UnitConversionApiRequestData request)
        {
            string conversionResponse;
            decimal result;

            // If we get here then we are sure the metric key, from metric and to metric fields provided are correct and exist in db
            if (request.MetricKey == "CelsFahr" || request.MetricKey == "FahrCels")
            {
                (string msg, decimal val) = ProcessTemperature(conversion, request.FromMetricValue, request.ToMetric, request.FromMetric);
                conversionResponse = msg;
                result = val;
            } else
            {
                (string msg, decimal val) = ProcessOtherConversionRates(conversion, request.FromMetricValue, request.ToMetric, request.FromMetric);
                conversionResponse = msg;
                result = val;
            }

            return (conversionResponse, result);
        }

        public static (string, decimal) ProcessTemperature(UnitConversion conversion, decimal fromMetricValue, string toMetric, string fromMetric)
        {
            decimal result;

            // this operator is for celcius to farenheit which exist in the db
            if (conversion.RateOperator == "MultiplicationAddition")
            {
                result = fromMetricValue * conversion.Rate + conversion.SecondRate;
            } else // use farenheit to celcius conversion rate operation if no match exists for operator from celcius to farenheit
            {
                result = (fromMetricValue - conversion.Rate) * conversion.SecondRate;
            }
            result = Math.Round(result, 2); // we are rounding to 2 dps for temperature conversions
            return (string.Format("Conversion of {0} to {1} is: {2} {1}", fromMetric, toMetric, result), result);
        }

        public static (string, decimal) ProcessOtherConversionRates(UnitConversion conversion, decimal fromMetricValue, string toMetric, string fromMetric)
        {
            decimal result;
            result = fromMetricValue * conversion.Rate;
            result = Math.Round(result, 4); // we are rounding to 4 dps for other unit conversions
            return (string.Format("Conversion of {0} to {1} is: {2} {1}", fromMetric, toMetric, result), result);
        }

        public static UnitConversionResponseData UnitConversionBadRequest(string message)
        {
            var response = new UnitConversionResponseData();
            response.message = message;
            response.statusCode = HttpStatusCode.BadRequest;
            return response;
        }
    }
}
