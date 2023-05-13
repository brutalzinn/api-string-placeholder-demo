using ApiPlaceHolderDemo.Integrations.ApiPlaceHolderDemo.Models;
using RestEase;
using System.Threading.Tasks;

namespace ApiPlaceHolderDemo.Integrations.ApiPlaceHolderDemo
{
    public interface IApiPlaceHolderDemoApi
    {
        [Header("ApiKey")]
        string ApiKey { get; set; }

        [Get("/obterCNPJValido/{socio}/{situacao}/{normalizado}/{excluirEmpresa}")]
        Task<ObterCNPJValidoResponse> ObterDadoEmpresa([Path] string socio, [Path] string situacao, [Path] bool normalizado = true, [Path] bool excluirEmpresa = false);
    }
}
