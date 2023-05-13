using ApiPlaceHolderDemo.Models;
using ApiPlaceHolderDemo.Services.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPlaceHolderDemo.Routes.Mocks.Documento
{
    public static class DocumentoMockRoute
    {
        public static void CriarRota(this WebApplication app)
        {
            app.MapGet("/mocks/obterdocumento/{DocumentType}",
            ([FromRoute] DocumentType DocumentType, IGeradorDocumentoMock geradorDocumentoMock) =>
            {
                var documento = geradorDocumentoMock.GenerateDocument(DocumentType);
                return documento;
            }).WithTags("Mocks")
            .WithOpenApi(options =>
            {
                options.Summary = "Obtém uma imagem base64 de um documento com pontos aleatórios na imagem.";
                return options;
            });
        }
    }

}