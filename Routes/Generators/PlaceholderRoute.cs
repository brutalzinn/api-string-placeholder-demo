using ApiPlaceHolderDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using StringPlaceholder;
using System.Collections.Generic;
using System.IO;

namespace ApiPlaceHolderDemo.Routes.Geradores
{
    public static class PlaceholderRoute
    {
        public static void CriarRota(this WebApplication app)
        {
            app.MapPost("/v2/placeholder",
               [Authorize(AuthenticationSchemes = "ApiKey")]
            async (HttpRequest httpContext, [FromServices] Placeholder placeHolder) =>
               {
                   var customPlaceholders = new List<StringExecutor>();
                   foreach (var queryParam in httpContext.Query)
                   {
                       customPlaceholders.Add(new StringExecutor(queryParam.Key, () => queryParam.Value));
                   }
                   var data = "";
                   using (StreamReader stream = new StreamReader(httpContext.Body))
                   {
                       data = await stream.ReadToEndAsync();
                   }
                   var resultado = placeHolder.GetTextWithCustomPlaceholders(data, customPlaceholders);
                   return Results.Content(resultado);
               }).WithTags("Mocks")
                .WithOpenApi(options =>
                {
                    var placeholder = app.Services.GetRequiredService<Placeholder>();
                    var descricao = placeholder.GetPlaceholdersDescriptionHTML();
                    options.Summary = "Create placeholders with custom vars";
                    options.Description = $"{descricao}";
                    return options;
                });


            app.MapPost("/placeholder",
                [Authorize(AuthenticationSchemes = "ApiKey")]
            async (HttpRequest httpContext, [FromServices] Placeholder placeHolder) =>
                {
                    var data = "";
                    using (StreamReader stream = new StreamReader(httpContext.Body))
                    {
                        data = await stream.ReadToEndAsync();
                    }
                    var resultado = placeHolder.GetText(data);
                    return Results.Content(resultado);
                }).WithTags("Mocks")
                 .WithOpenApi(options =>
                 {
                     var placeholder = app.Services.GetRequiredService<Placeholder>();
                     var descricao = placeholder.GetPlaceholdersDescriptionHTML();
                     options.Summary = "Create a generic placeholders with a pre set of placeholders.";
                     options.Description = $"{descricao}";
                     return options;
                 });

            app.MapGet("/placeholder",
                   [Authorize(AuthenticationSchemes = "ApiKey")]
            (HttpRequest httpContext, [FromServices] Placeholder placeHolder) =>
                   {
                       var placeholders = placeHolder.GetPlaceholdersDescriptions();
                       return placeholders;
                   }).WithTags("Mocks")
                    .WithOpenApi(options =>
                    {
                        options.Summary = "Get list of available placeholders";
                        return options;
                    });
        }
    }
}
