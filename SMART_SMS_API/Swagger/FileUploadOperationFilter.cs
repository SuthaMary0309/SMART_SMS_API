using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SMART_SMS_API.Swagger
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var methodParams = context.MethodInfo.GetParameters();
            var fileParams = methodParams
                .Where(p => p.ParameterType == typeof(IFormFile) || 
                           p.ParameterType == typeof(IFormFileCollection))
                .ToList();

            if (!fileParams.Any())
                return;

            // Create multipart/form-data schema
            var properties = new Dictionary<string, OpenApiSchema>();
            var required = new HashSet<string>();

            foreach (var param in fileParams)
            {
                var paramName = param.Name ?? "file";
                properties[paramName] = new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                };
                
                if (!param.IsOptional && !param.HasDefaultValue)
                {
                    required.Add(paramName);
                }
            }

            // Set request body
            operation.RequestBody = new OpenApiRequestBody
            {
                Required = true,
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = properties,
                            Required = required.Count > 0 ? required : null
                        }
                    }
                }
            };

            // Remove IFormFile parameters from operation.Parameters
            var paramsToRemove = new List<OpenApiParameter>();
            foreach (var param in operation.Parameters)
            {
                var methodParam = methodParams.FirstOrDefault(p => p.Name == param.Name);
                if (methodParam != null && 
                    (methodParam.ParameterType == typeof(IFormFile) || 
                     methodParam.ParameterType == typeof(IFormFileCollection)))
                {
                    paramsToRemove.Add(param);
                }
            }

            foreach (var param in paramsToRemove)
            {
                operation.Parameters.Remove(param);
            }
        }
    }
}
