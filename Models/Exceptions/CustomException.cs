using System;

namespace ApiPlaceHolderDemo.Models.Exceptions
{
    [Serializable]
    public class CustomException : Exception
    {
        public int StatusCode { get; }
        public TypeExcecao Type { get; }

        public CustomException(TypeExcecao TypeExcecao, string message)
       : base(message)
        {
            Type = TypeExcecao;
            StatusCode = (int)TypeExcecao;
        }

        public CustomExceptionResponse ObterResponse()
        {
            var response = new CustomExceptionResponse()
            {
                Type = GetType(),
                Message = base.Message
            };

            return response;
        }

        private string GetType()
        {
            switch (Type)
            {
                case TypeExcecao.NEGOCIO:
                    return "business";
                case TypeExcecao.AUTORIZACAO:
                    return "not_authorize";
                case TypeExcecao.VALIDACAO:
                    return "validation";

            }
            return "not_recognized";
        }


    }
    public class CustomExceptionResponse
    {
        public string Type { get; set; }
        public string Message { get; set; }
    }
}
