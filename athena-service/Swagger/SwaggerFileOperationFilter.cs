using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AthenaService.Swagger
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType.FullName != null && p.ParameterType.FullName.Equals(typeof(IFormFile).FullName));
            if (fileParams.Count() == 1)
            {
                var openApiMediaType = new OpenApiMediaType()
                {
                    Schema = new OpenApiSchema()
                    {
                        Type = "object",
                        Properties =
                        {
                            ["file"] = new OpenApiSchema() { Description = "Select file", Type = "string", Format = "binary" }
                        }
                    }
                };

                operation.RequestBody = new OpenApiRequestBody()
                {
                    Content =
                    {
                        ["multipart/form-data"] = openApiMediaType
                    }
                };
            }
        }
    }
}
