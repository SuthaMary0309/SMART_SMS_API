using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace SMART_SMS_API.Swagger
{
    public class FileUploadSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == null) return;

            // Handle IFormFile directly
            if (context.Type == typeof(IFormFile))
            {
                schema.Type = "string";
                schema.Format = "binary";
                return;
            }

            // Handle nullable IFormFile
            if (context.Type.IsGenericType && 
                context.Type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                context.Type.GetGenericArguments()[0] == typeof(IFormFile))
            {
                schema.Type = "string";
                schema.Format = "binary";
                schema.Nullable = true;
                return;
            }

            // Handle complex types with IFormFile properties
            if (schema.Properties != null && context.Type.GetProperties() != null)
            {
                var properties = context.Type.GetProperties();
                foreach (var prop in properties)
                {
                    var propType = prop.PropertyType;
                    var isFormFile = propType == typeof(IFormFile) ||
                                    (propType.IsGenericType &&
                                     propType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                                     propType.GetGenericArguments()[0] == typeof(IFormFile));

                    if (isFormFile && schema.Properties.ContainsKey(prop.Name))
                    {
                        schema.Properties[prop.Name].Type = "string";
                        schema.Properties[prop.Name].Format = "binary";
                        if (propType.IsGenericType)
                        {
                            schema.Properties[prop.Name].Nullable = true;
                        }
                    }
                }
            }
        }
    }
}

