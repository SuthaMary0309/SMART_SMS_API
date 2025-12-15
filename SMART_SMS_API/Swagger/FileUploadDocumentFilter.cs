using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace SMART_SMS_API.Swagger
{
    public class FileUploadDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var path in swaggerDoc.Paths.Values)
            {
                foreach (var operation in path.Operations.Values)
                {
                    // Find IFormFile parameters
                    var fileParams = operation.Parameters?
                        .Where(p => p.Schema?.Format == "binary" || 
                                   p.Name?.ToLower().Contains("file") == true)
                        .ToList();

                    if (fileParams != null && fileParams.Any())
                    {
                        // Convert to request body
                        var properties = new Dictionary<string, OpenApiSchema>();
                        var required = new HashSet<string>();

                        foreach (var param in fileParams)
                        {
                            properties[param.Name] = new OpenApiSchema
                            {
                                Type = "string",
                                Format = "binary"
                            };
                            if (param.Required)
                            {
                                required.Add(param.Name);
                            }
                        }

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

                        // Remove file parameters
                        foreach (var param in fileParams)
                        {
                            operation.Parameters.Remove(param);
                        }
                    }
                }
            }
        }
    }
}

