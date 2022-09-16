using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;


namespace MetricsConversion.Data.Core
{
    public class HeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null) return;

            var actionName = controllerActionDescriptor.ActionName;
            var resourceName = controllerActionDescriptor.ControllerName.TrimEnd('s');

            if (resourceName.StartsWith("Token"))
            {

                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Clear();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "X-Authorization", // Request header name 
                    In = ParameterLocation.Header, // Type of the parameter
                    Description = "Basic U3dhZ2dlcjpUZXN0", // Description of the parameter
                    Required = true,  // Whether it is mandatory or not
                    Schema = new OpenApiSchema // Parameter variable format 
                    {
                        Title = "Basic",
                        Type = "string",

                    },
                    Style = ParameterStyle.Simple
                });
            }
        }
    }
}
