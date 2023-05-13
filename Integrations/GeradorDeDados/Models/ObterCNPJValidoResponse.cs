using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiPlaceHolderDemo.Integrations.GeradorDeDados.Models
{
    public class ObterCNPJValidoResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("ultimaAtualizacao")]
        public DateTime UltimaAtualizacao { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("tipo")]
        public string Tipo { get; set; }

        [JsonProperty("porte")]
        public string Porte { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("fantasia")]
        public string Fantasia { get; set; }

        [JsonProperty("abertura")]
        public string Abertura { get; set; }

        [JsonProperty("atividadePrincipal")]
        public List<AtividadePrincipal> AtividadePrincipal { get; set; }

        [JsonProperty("atividadesSecundarias")]
        public List<AtividadesSecundaria> AtividadesSecundarias { get; set; }

        [JsonProperty("naturezaJuridica")]
        public string NaturezaJuridica { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("municipio")]
        public string Municipio { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }

        [JsonProperty("efr")]
        public string Efr { get; set; }

        [JsonProperty("situacao")]
        public string Situacao { get; set; }

        [JsonProperty("dataSituacao")]
        public string DataSituacao { get; set; }

        [JsonProperty("motivoSituacao")]
        public string MotivoSituacao { get; set; }

        [JsonProperty("situacaoEspecial")]
        public string SituacaoEspecial { get; set; }

        [JsonProperty("dataSituacaoEspecial")]
        public string DataSituacaoEspecial { get; set; }

        [JsonProperty("capitalSocial")]
        public string CapitalSocial { get; set; }

        [JsonProperty("qsa")]
        public List<Qsa> Qsa { get; set; }

        [JsonProperty("billing")]
        public Billing Billing { get; set; }
    }
    public class AtividadePrincipal
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class AtividadesSecundaria
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Billing
    {
        [JsonProperty("free")]
        public bool Free { get; set; }

        [JsonProperty("database")]
        public bool Database { get; set; }
    }

    public class Qsa
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("qual")]
        public string Qual { get; set; }

        [JsonProperty("paisOrigem")]
        public object PaisOrigem { get; set; }

        [JsonProperty("nomeRepLegal")]
        public object NomeRepLegal { get; set; }

        [JsonProperty("qualRepLegal")]
        public object QualRepLegal { get; set; }
    }

}
