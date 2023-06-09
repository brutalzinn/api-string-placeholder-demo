﻿using Bogus;
using Bogus.Extensions;
using Bogus.Extensions.Brazil;
using ApiPlaceHolderDemo.Models;
using ApiPlaceHolderDemo.Models.Exceptions;
using ApiPlaceHolderDemo.Services.Mocks;
using StringPlaceholder;
using StringPlaceholder.FluentPattern;
using StringPlaceholder.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using ApiPlaceHolderDemo.Integrations.ApiPlaceHolderDemo;

namespace ApiPlaceHolderDemo.Services
{
    public class Placeholder
    {
        private readonly IExecutorCreator executorCreator;
        private readonly IApiPlaceHolderDemoApi apiPlaceHolderDemoApi;
        public Placeholder(IExecutorCreator executorCreator, IApiPlaceHolderDemoApi apiPlaceHolderDemoApi)
        {
            this.executorCreator = executorCreator;
            this.apiPlaceHolderDemoApi = apiPlaceHolderDemoApi;
        }

        public string GetTextWithCustomPlaceholders(string text, List<StringExecutor> customPlaceholders)
        {

            var newText = new ExecutorCreator()
                .Init()
                .AddRange(Placeholder.GetDefaultExecutors(apiPlaceHolderDemoApi))
                .AddRange(customPlaceholders)
                .Build(text)
                    .Result();
            return newText;
        }

        public string GetText(string texto)
        {
            var novoTexto = executorCreator.Build(texto)
                .Result();
            return novoTexto;
        }

        public IEnumerable<DescriptionModel> GetPlaceholdersDescriptions()
        {
            var descricoes = executorCreator.GetDescription();
            return descricoes;
        }
        public string GetPlaceholdersDescriptionHTML()
        {
            var descricaoPlaceholders = "Avaible placeholders:<br/><br/>";
            var descricoes = executorCreator.GetDescription();
            foreach (var item in descricoes)
            {
                var aceitaParametros = item.MultipleParams;
                var chave = item.Key;
                var descricao = item.Description;
                var args = item.Args;
                var listaParametros = string.Join(",", args);
                var casoDeUso = $"[{chave}]";
                if (aceitaParametros)
                {
                    casoDeUso = $"[{chave}({listaParametros})]";
                }
                descricaoPlaceholders += $"{chave}" +
                    "<br/>" +
                    $"Uso: {casoDeUso}" +
                    "<br/>" +
                    $"Descrição: {descricao}<br/>" +
                    "<br/>";

            }
            return descricaoPlaceholders;
        }

        public static List<StringExecutor> GetDefaultExecutors(IApiPlaceHolderDemoApi geradorDeDados)
        {
            return new List<StringExecutor>()
            {
                new StringExecutor("GUID", ()=> Guid.NewGuid().ToString(), "Cria um UUID padrão"),
                new StringExecutor("LOGRADOURO", ()=> new Faker().Address.StreetAddress(false), "Cria um logradouro"),
                new StringExecutor("CEP", ()=> new Faker().Address.ZipCode("########"), "Cria um cep"),
                new StringExecutor("EMAIL", ()=> new Faker().Person.Email.ToLower(), "Cria um email"),
                new StringExecutor("CIDADE", ()=> new Faker().Address.City(), "Cria uma cidade"),
                new StringExecutor("BAIRRO", ()=> new Faker().Address.StreetName(), "Cria um bairro"),
                new StringExecutor("DINHEIRO", ()=>  new Faker().Random.Decimal2(100,5000).ToString("0.##")),
                new StringExecutor("NOME_COMPLETO", ()=> new Faker().Name.FullName(), "Cria um nome completo de pessoa"),
                new StringExecutor("RG", ()=> new Faker().Random.String2(8, "0123456789"), "Cria um RG"),
                new StringExecutor("CPF", ()=> new Faker().Person.Cpf(false), "Cria um CPF"),
                new StringExecutor("CNPJ",()=> new Faker().Company.Cnpj(false), "Cria um CNPJ"),
                new StringExecutor("CNPJ_RECEITAWS_QSA_UM", ()=>  geradorDeDados.ObterDadoEmpresa("UnicoSocio", "Ativa", true, false).Result.Cnpj, "Cria um CNPJ usando a ReceitaWS com único sócio"),
                new StringExecutor("CNPJ_RECEITAWS_QSA_VARIOS", () => geradorDeDados.ObterDadoEmpresa("VariosSocios", "Ativa", true, false).Result.Cnpj, "Cria um CNPJ usando a ReceitaWS com mais de um sócio"),
                new StringExecutor("CNPJ_RECEITAWS_ALEATORIO", () => geradorDeDados.ObterDadoEmpresa("Aleatorio", "Aleatorio", true, false).Result.Cnpj, "Cria um CNPJ usando a ReceitaWS com mais de um sócio"),
                new StringExecutor("COMPANY_NAME",()=> new Faker().Company.CompanyName(), "Cria um nome de empresa aleatório."),
                new StringExecutor("FILE_URL_BASE64", GetFileUrlBase64, "Cria um base64 dado a url de um arquivo como parâmetro.",
                new List<string>()
                {
                "imageUrl"
                }),
                  new StringExecutor("IMAGEM_DOCUMENTO_MOCK", GenerateRandomImageWithPixelsDots, "Cria uma imagem com pixels gerados aleatoriamente</br>" +
                  "arquivos disponíveis: </br>" +
                  "rg_frente</br>" +
                  "rg_verso</br>"+
                  "selfie",
                new List<string>()
                {
                "DocumentType"
                }),
                new StringExecutor("PDF_DOCUMENTO_MOCK", GenerateRandomDocumentPDF, "Cria um pdf com tamanho específico de um lorem ipsum dor em KBS </br>",
                new List<string>()
                {
                "tamanho_em_kbs"
                })
            };
        }
        private static string GetFileUrlBase64(string[] strParams)
        {
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                var bytes = client.GetByteArrayAsync(strParams[0]).Result;
                return Convert.ToBase64String(bytes);
            }
        }
        private static string GenerateRandomImageWithPixelsDots(string[] strParams)
        {
            var DocumentType = Utils.GetEnumByDescription<DocumentType>(strParams[0]);
            var geradorDeDocumento = new GeradorDocumentoMock();
            var documento = geradorDeDocumento.GenerateDocument(DocumentType);
            var resultado = documento.Base64;
            return resultado;
        }
        private static string GenerateRandomDocumentPDF(string[] strParams)
        {
            var parametroTamanho = strParams[0];
            int tamanhoKbs = 80;
            if (!string.IsNullOrEmpty(parametroTamanho))
            {
                var parser = int.TryParse(parametroTamanho, out tamanhoKbs);
                if (!parser)
                {
                    throw new CustomException(Models.TypeExcecao.NEGOCIO, "Não foi possível determinar o parâmetro informado.");
                }
            }

            var geradorDeDocumento = new GeradorDocumentoMock();
            var documento = geradorDeDocumento.GenerateRandomPdfWithFixedTextLenght(DocumentType.PDF, tamanhoKbs);
            var resultado = documento.Base64;
            return resultado;
        }
    }
}
