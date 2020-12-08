using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Travel.Communication.Email;
using Travel.Data.Access.DAL;
using Travel.Filters;
using Travel.Helpers;
using Travel.Maps;
using Travel.Queries.Queries;
using Travel.Security;
using Travel.Security.Auth;

namespace Travel.IoC
{
    public static class ContainerSetup
    {
     
        public static void SetUp(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureAuth(services);
            ConfigureAutoMapper(services);
            AddQueries(services);
            AddUnitOfWork(services, configuration);
            ConfigureCors(services);
            AddServices(services);
            ConfigureIISIntegration(services);
        }

        private static void ConfigureIISIntegration(IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        private static void ConfigureAuth(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<ISecurityContext, SecurityContext>();
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
        }
        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            var mappingConfig = AutoMapperConfigurator.Configure();
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(x => mapper);
            services.AddTransient<IAutoMapper, AutoMapperAdapter>();
        }

        private static void AddUnitOfWork(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Data:main"];
             services.AddDbContext<MainDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork>(uow => new EFUnitOfWork(uow.GetRequiredService<MainDbContext>()));
        }

        private static void AddQueries(IServiceCollection services)
        {
            var exampleProcessorType = typeof(UserQueryProcessor);
            var types = (from t in exampleProcessorType.GetTypeInfo().Assembly.GetTypes()
                where t.Namespace == exampleProcessorType.Namespace
                      && t.GetTypeInfo().IsClass
                      && t.GetTypeInfo().GetCustomAttribute<CompilerGeneratedAttribute>() == null
                select t).ToArray();

            foreach (var type in types)
            {
                var interfaceQ = type.GetTypeInfo().GetInterfaces().First();
                services.AddScoped(interfaceQ, type);
            }
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
        }

    }
}
