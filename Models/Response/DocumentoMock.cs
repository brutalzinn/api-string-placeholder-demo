namespace ApiPlaceHolderDemo.Models.Response
{
    public class DocumentoMock
    {
        public DocumentType DocumentType { get; set; }
        public string Base64 { get; set; }
        public DocumentoMock(DocumentType DocumentType, string base64)
        {
            DocumentType = DocumentType;
            Base64 = base64;
        }
    }
}
