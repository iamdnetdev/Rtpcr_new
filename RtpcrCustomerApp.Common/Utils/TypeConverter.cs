using System;
using System.Text.RegularExpressions;

namespace RtpcrCustomerApp.Common.Utils
{
    public static class TypeConverter
    {
        public static bool TryChangeType<T>(object value, out T result) 
        {
            try
            {
                if(value == null)
                {
                    result = default(T);
                    return false;
                }
                if (typeof(T) is IConvertible)
                {
                    result = (T)Convert.ChangeType(value, typeof(T));
                    return true;
                }
                else if (typeof(T) == typeof(Int32) || typeof(T) == typeof(Int16))
                {
                    result = (T)(object)Convert.ToInt32(value.ToString());
                    return true;
                }
                else if(typeof(T) == typeof(Guid))
                {                    
                    result = (T)(object)Guid.Parse(value.ToString());
                    return true;
                }
                else
                {
                    result = default(T);
                    return false;
                }
            }
            catch
            {
                result = default(T);
                return false;
            }
        }
    }
}
