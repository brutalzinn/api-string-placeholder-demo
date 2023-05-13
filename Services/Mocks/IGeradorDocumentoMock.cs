using ApiPlaceHolderDemo.Models;
using ApiPlaceHolderDemo.Models.Response;

namespace ApiPlaceHolderDemo.Services.Mocks
{
    public interface IGeradorDocumentoMock
    {
        DocumentoMock GenerateDocument(DocumentType DocumentType);
        DocumentoMock GenerateImageWithRandomPixels(DocumentType DocumentType);
        DocumentoMock GenerateRandomPdfWithFixedTextLenght(DocumentType DocumentType, int loremIpsumEmKbs);

    }
}
