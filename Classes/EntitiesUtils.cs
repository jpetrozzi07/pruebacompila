using System;
using System.Reflection;

namespace TaskIlu14Cti.Classes
{
    public static class EntitiesUtils
    {
        public static object GetPropValue(object src, string propName)
        {
            PropertyInfo prop = src.GetType().GetProperty(propName);
            if (prop == null)
                return null;
            return prop.GetValue(src, null);
        }

        public static string GetPropValueSqlString(object src, string propName)
        {
            PropertyInfo prop = src.GetType().GetProperty(propName);
            if (prop == null)
                return "NULL";


            string fieldValue = string.Empty;
            object value = prop.GetValue(src, null);


            if (value == null)
                fieldValue = "Null";
            else if (value is string)
                fieldValue = $"'{value.ToString()}'";
            else if (value is DateTime)
                fieldValue = $"'{((DateTime)value).ToString()}'";
            else
                fieldValue = value.ToString();

            return fieldValue;
        }
    }
}
