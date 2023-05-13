using ApiPlaceHolderDemo.Models.Exceptions;
using System;
using System.ComponentModel;
using System.Linq;

namespace ApiPlaceHolderDemo
{
    public static class Utils
    {


        public static string GetDescriptionEnum(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
            {
                throw new CustomException(Models.TypeExcecao.NEGOCIO, $"Não foi possível obter esse enum.");
            }
            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static T GetEnumByDescription<T>(string description) where T : Enum
        {
            var enumType = typeof(T);
            var matchingValue = Enum.GetValues(enumType).Cast<T>().FirstOrDefault(enumValue =>
            {
                var attribute = enumType.GetField(enumValue.ToString())
                                         .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                         .Cast<DescriptionAttribute>()
                                         .SingleOrDefault();

                return attribute != null && attribute.Description.Equals(description, StringComparison.OrdinalIgnoreCase);
            });
            return matchingValue ?? throw new CustomException(Models.TypeExcecao.NEGOCIO, $"Não existe {enumType.Name} com essa descrição:'{description}'");
        }
    }
}
