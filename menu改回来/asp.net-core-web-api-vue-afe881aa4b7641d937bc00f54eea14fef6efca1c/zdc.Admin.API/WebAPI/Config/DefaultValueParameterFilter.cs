using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace WebAPI.Config
{
    public class DefaultValueParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            // 设置普通的string类型默认值
            if (parameter.In == ParameterLocation.Query && parameter.Schema.Type == "string")
            {
                parameter.Example = new OpenApiString("value111111");   
            }
        }
    }


    /// <summary>
    /// 对象类型的过滤器
    /// </summary>
    public class DefaultValueSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema == null)
            {
                return;
            }
            var objectSchema = schema;
            foreach (var property in objectSchema.Properties)
            {
                // 按照数据的类型去指定默认值
                if (property.Value.Type == "string" && property.Value.Default == null)
                {
                    property.Value.Default = new OpenApiString("");
                }
                // 按照字段名去指定类型
                else if (property.Key == "pageIndex")
                {
                    property.Value.Example = new OpenApiInteger(1);
                }
                else if (property.Key == "pageSize")
                {
                    property.Value.Example = new OpenApiInteger(10);
                }
                // 通过特性来实现
                DefaultValueAttribute defaultAttribute = context.ParameterInfo?.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultAttribute != null)
                {
                    property.Value.Example = (IOpenApiAny)defaultAttribute.Value;
                }
            }
        }
    }
}
