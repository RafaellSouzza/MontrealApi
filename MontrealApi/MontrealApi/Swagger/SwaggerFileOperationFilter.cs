using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MontrealApi.Swagger
{

    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var formFileParams = context.MethodInfo
                .GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile) || p.ParameterType == typeof(IFormFileCollection))
                .ToList();

            if (!formFileParams.Any())
                return;

            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = formFileParams.ToDictionary(param => param.Name, param => new OpenApiSchema { Type = "string", Format = "binary" })
                    }
                }
            }
            };
        }
    }
}
