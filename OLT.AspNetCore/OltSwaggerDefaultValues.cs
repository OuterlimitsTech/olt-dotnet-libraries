using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OLT.Core
{
    /// <summary>  
    /// Represents the Swagger/Swashbuckle operation filter used to document the implicit API version parameter.  
    /// </summary>  
    /// <remarks>This <see cref="SwaggerGenerator"/> is only required due to bugs in the <see cref="IOperationFilter"/>.  
    /// Once they are fixed and published, this class can be removed.</remarks>  
    public class OltSwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters)
            {
                var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                var routeInfo = description.RouteInfo;
                if (string.IsNullOrEmpty(parameter.Name))
                {
                    parameter.Name = description.ModelMetadata?.Name;
                }

                parameter.Description ??= description.ModelMetadata?.Description;

                if (parameter.Name == "api-version")
                {
                    parameter.Required = description.IsRequired;
                    parameter.AllowEmptyValue = false;
                    if (description.DefaultValue != null)
                    {
                        parameter.Example = new OpenApiString(description.DefaultValue.ToString());
                    }
                }


                if (routeInfo == null)
                {
                    continue;
                }


                parameter.Required |= !routeInfo.IsOptional;
            }

        }

    }
}