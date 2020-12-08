using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Travel.Filters
{
    public class CustomSwaggerDocumentAttribute: IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
           
            swaggerDoc.Info = new OpenApiInfo
            {
                Title = "Cervidae Experience Travels",
                Version = "v1.0.0",
                Description = "Services and apis",
                Contact = new OpenApiContact
                {
                    Name = "ndu systems",
                    Email = "ndu.systems@gmail.com"
                }
            };
        }
    }
}
