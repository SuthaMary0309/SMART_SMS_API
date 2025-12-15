using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SMART_SMS_API.Swagger
{
    public class FileUploadParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (context.ParameterInfo?.ParameterType == typeof(IFormFile) ||
                context.ParameterInfo?.ParameterType == typeof(IFormFileCollection))
            {
                // Convert to form data parameter
                parameter.In = ParameterLocation.Query;
                parameter.Schema = new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                };
            }
        }
    }
}

