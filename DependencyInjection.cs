using ConfigurationSubstitution;
using Cronos;
using ApiPlaceHolderDemo.Authentication;
using ApiPlaceHolderDemo.Models.Settings;
using ApiPlaceHolderDemo.Services;
using ApiPlaceHolderDemo.Services.Mocks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using RestEase.HttpClientFactory;
using StringPlaceholder.FluentPattern;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using ApiPlaceHolderDemo.Integrations.ApiPlaceHolderDemo;

namespace ApiPlaceHolderDemo
{
    public static class DependencyInjection
    {
        public static void Inject(this IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables()
                 .EnableSubstitutions("%", "%")
                 .Build();

            services.InjectConfigurations(config);
            services.InjectAuthentions();
            services.InjectServices();
            services.InjectIntegrations();
            services.InjectPlaceholderPackage();
            services.InjectSwagger();
            services.InjectCors();
        }
        private static void InjectServices(this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddSingleton<IGeradorDocumentoMock, GeradorDocumentoMock>();
            services.AddSingleton<ApiCicloDeVida>();
        }

        private static void InjectIntegrations(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var apiConfig = serviceProvider.GetRequiredService<IOptions<ApiConfig>>().Value;
            var geradorDeDadosApiUrl = apiConfig.Integrations.ApiApiPlaceHolderDemoConfig.ApiUrl;
            var geradorDeDadosApiKey = apiConfig.Integrations.ApiApiPlaceHolderDemoConfig.ApiKey;
            services.AddRestEaseClient<IApiPlaceHolderDemoApi>(geradorDeDadosApiUrl, new()
            {
                InstanceConfigurer = instance => instance.ApiKey = geradorDeDadosApiKey,
            });
        }

        private static void InjectPlaceholderPackage(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var apiApiPlaceHolderDemo = serviceProvider.GetRequiredService<IApiPlaceHolderDemoApi>();
            var stringPlaceHolder = new ExecutorCreator().Init()
                .AddRange(Placeholder.GetDefaultExecutors(apiApiPlaceHolderDemo))
                .BuildDescription();

            services.AddSingleton<Placeholder>();
            services.AddSingleton(stringPlaceHolder);
        }
        private static void InjectAuthentions(this IServiceCollection services)
        {
            services.AddAuthentication("ApiKey")
                 .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>
                 ("ApiKey", null);

        }

        private static void InjectConfigurations(this IServiceCollection services, IConfigurationRoot config)
        {
            services.Configure<ApiConfig>(options => config.GetSection("ApiConfig").Bind(options));
        }

        private static void InjectCors(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var apiConfig = serviceProvider.GetRequiredService<IOptions<ApiConfig>>().Value;
            services.AddCors(p => p.AddPolicy("corsapp",
            builder =>
            {
                builder
                .WithOrigins(apiConfig.CorsOrigin)
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
        }

        private static void InjectSwagger(this IServiceCollection services)
        {
            var contact = new OpenApiContact()
            {
                Name = "Roberto Paes",
                Email = "contato@robertinho.net",
                Url = new Uri("https://robertocpaes.dev")
            };
            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Api string placeholders (DEMO)",
                Description = "Minimal API to generate process a text with placeholders",
                Contact = contact
            };
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<EnumSchemaFilter>();
                c.SwaggerDoc("v1", info);
                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "Insert the apikey above.",
                    Type = SecuritySchemeType.ApiKey,
                    Name = "ApiKey",
                    In = ParameterLocation.Header,
                    Scheme = "ApiKeyScheme"
                });
                var key = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    In = ParameterLocation.Header
                };
                var requirement = new OpenApiSecurityRequirement
                    {
                             { key, new List<string>() }
                    };
                c.AddSecurityRequirement(requirement);
            });
        }
        internal sealed class EnumSchemaFilter : ISchemaFilter
        {
            public void Apply(OpenApiSchema model, SchemaFilterContext context)
            {
                if (context.Type.IsEnum)
                {
                    model.Enum.Clear();
                    Enum
                       .GetNames(context.Type)
                       .ToList()
                       .ForEach(name => model.Enum.Add(new OpenApiString($"{name}")));
                    model.Type = "string";
                    model.Format = string.Empty;
                }

            }
        }
    }
}
