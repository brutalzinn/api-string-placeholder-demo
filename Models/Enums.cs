using System.ComponentModel;

namespace ApiPlaceHolderDemo.Models
{
    public enum FiltroSocio
    {
        Aleatorio = 0,
        UnicoSocio,
        VariosSocios
    }
    public enum FiltroSituacao
    {
        Aleatorio = 0,
        Ativa,
        Baixada
    }
    public enum TypeExcecao
    {
        VALIDACAO = 400,
        NEGOCIO = 406,
        NOTFOUND = 404,
        AUTORIZACAO = 401,
        INTERNO = 500
    }
    public enum DocumentType
    {
        [Description("selfie")]
        SELFIE = 1,
        [Description("rg_frente")]
        RG_FRENTE,
        [Description("rg_verso")]
        RG_VERSO,
        [Description("pdf")]
        PDF
    }
}
