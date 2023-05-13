using ApiPlaceHolderDemo;
using ApiPlaceHolderDemo.Handlers;
using ApiPlaceHolderDemo.Models.Settings;
using ApiPlaceHolderDemo.Routes.Geradores;
using ApiPlaceHolderDemo.Routes.Mocks.Documento;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        DependencyInjection.Inject(builder.Services);
        var app = builder.Build();
        var apiConfig = app.Services.GetRequiredService<IOptions<ApiConfig>>().Value;
        app.UseCors("corsapp");

        app.UseAuthentication();
        app.UseAuthorization();
        app.AddCustomExceptionHandler();
        if (apiConfig.Swagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        PlaceholderRoute.CriarRota(app);
        DocumentoMockRoute.CriarRota(app);
        app.Run();
    }
}

