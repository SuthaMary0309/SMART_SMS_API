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
            if (context.Type == typeof(IFormFile))
            {
                schema.Type = "string";
                schema.Format = "binary";
                return;
            }

            if (schema.Properties == null)
                return;

            var properties = context.Type.GetProperties();

            foreach (var prop in properties)
            {
                if (prop.PropertyType == typeof(IFormFile) &&
                    schema.Properties.ContainsKey(prop.Name))
                {
                    schema.Properties[prop.Name].Type = "string";
                    schema.Properties[prop.Name].Format = "binary";
                }
            }
        }

    }
}

