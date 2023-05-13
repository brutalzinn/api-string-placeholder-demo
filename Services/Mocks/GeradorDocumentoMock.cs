using ApiPlaceHolderDemo.Mocks.Utils;
using ApiPlaceHolderDemo.Models;
using ApiPlaceHolderDemo.Models.Response;
using System.Collections.Generic;
using System.IO;

namespace ApiPlaceHolderDemo.Services.Mocks
{
    public class GeradorDocumentoMock : IGeradorDocumentoMock
    {
        private List<DocumentType> documentosImagem = new List<DocumentType> { DocumentType.RG_FRENTE, DocumentType.RG_VERSO, DocumentType.SELFIE };
        private List<DocumentType> documentosPDF = new List<DocumentType> { DocumentType.PDF };

        public DocumentoMock GenerateDocument(DocumentType DocumentType)
        {
            if (documentosImagem.Contains(DocumentType))
            {
                var imagemComPixelsRandom = GenerateImageWithRandomPixels(DocumentType);
                return imagemComPixelsRandom;
            }
            var documentoPdfRandom = GenerateRandomPdfWithFixedTextLenght(DocumentType);
            return documentoPdfRandom;
        }

        public DocumentoMock GenerateImageWithRandomPixels(DocumentType DocumentType)
        {
            var caminhoDocumento = Path.Combine("Mocks", "Documento", DocumentType.GetDescriptionEnum() + ".jpg");
            var imagemBase64 = ImageUtil.GenerateRandomDots(caminhoDocumento);
            return new DocumentoMock(DocumentType, imagemBase64);
        }
        public DocumentoMock GenerateRandomPdfWithFixedTextLenght(DocumentType DocumentType, int loremIpsumEmKbs = 80)
        {
            var pdfBase64 = PdfUtil.Generate(loremIpsumEmKbs);
            return new DocumentoMock(DocumentType, pdfBase64);
        }
    }
}
