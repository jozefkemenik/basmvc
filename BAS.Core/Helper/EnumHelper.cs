using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

using BAS.Core.Localization;

namespace BAS.Core.Helper
{
    public static class EnumHelper
    {
        public static Dictionary<TEnum, string> GetEnumValues<TEnum>()
        {
            bool isEnum = IsEnum<TEnum>();

            if (!isEnum)
                throw new ArgumentException("T must be an enumerated type");

            var enumDictionary = new Dictionary<TEnum, string>();

            Type enumType = GetUnderlyingType<TEnum>();
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            foreach (var value in values)
            {
                string localizedString = GetLocalizedString<TEnum>(value);
                enumDictionary.Add(value, localizedString);
            }

            return enumDictionary;
        }

        public static bool IsEnum<T>()
        {
            return GetUnderlyingType<T>().IsEnum;
        }

        public static Type GetUnderlyingType<T>()
        {
            Type realModelType = typeof(T);
            Type underlyingType = Nullable.GetUnderlyingType(realModelType);

            if (underlyingType != null)
                return underlyingType;

            return realModelType;
        }

        private static string GetLocalizedString<TEnum>(TEnum value)
        {
            Type type = GetUnderlyingType<TEnum>();
            string resourceKey = string.Format("{0}.{1}", type.Name, value.ToString());

            // try to get value from resource file
            string stringValue = LookupResource("Enum."+resourceKey);

            if (stringValue == null)
            {
                stringValue = GetDescription<TEnum>(value);
            }

            return stringValue;
        }

        public static string GetDescription<TEnum>(object value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string LookupResource(string resourceKey)
        {
            return Resources.Resource.ResourceManager.GetString(resourceKey) ?? resourceKey;//DatabaseResourceManager.Instance.GetString(resourceKey)??resourceKey;
        }

    }
}
