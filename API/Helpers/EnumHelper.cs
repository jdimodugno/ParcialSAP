using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Api.Helpers
{
    public class JsonEnum
    {
        public string key;
        public int value;
        public string description;
    }

    public static class EnumHelper<T>
    {
        public static string GetEnumDescription(string value)
        {
            Type type = typeof(T);
            var name = Enum
                .GetNames(type)
                .Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null) return string.Empty;

            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

        public static List<JsonEnum> ToJson()
        {
            Type enumType = typeof(T);
            List<JsonEnum> enumEntries = new List<JsonEnum>();

            foreach (int val in Enum.GetValues(enumType))
            {
                string _key = Enum.GetName(enumType, val);

                enumEntries.Add(new JsonEnum
                {
                    value = val,
                    key = _key,
                    description = GetEnumDescription(_key)
                });
            }

            return enumEntries;
        }
    }
}
