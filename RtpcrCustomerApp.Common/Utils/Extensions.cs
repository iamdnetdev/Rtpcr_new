namespace RtpcrCustomerApp.Common.Utils
{
    using RtpcrCustomerApp.BusinessModels.Common;
    using RtpcrCustomerApp.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public static class Extensions
    {
        private const string GenericError = "Something went wrong! Please try later or get in touch with our support team if it still persists.";
        private static readonly Environments Environment;
        static Extensions()
        {
            Enum.TryParse(ConfigurationManager.AppSettings["Enviroment"] ?? "Dev", out Environment);
        }

        public static List<Error> GetErrors(this Exception ex, string module = null, string operation = null)
        {
            var error = new Error();

            var baseEx = ex.GetBaseException();
            if (baseEx.GetType() == typeof(SqlException))
            {
                var sqlEx = (SqlException)baseEx;
                // Handled errors from SP
                if (sqlEx.Number == 50001)
                {
                    error = new Error
                    {
                        Code = "50001",
                        Description = sqlEx.Message,
                        Message = sqlEx.Message,
                        Module = module,
                        Operation = operation,
                        Type = ErrorType.Fatal
                    };
                }
                else
                {
                    error = new Error
                    {
                        Code = sqlEx.Number.ToString(),
                        Description = Environment == Environments.Prod ? GenericError : sqlEx.ToString(),
                        Message = Environment == Environments.Prod ? GenericError : sqlEx.Message,
                        Module = module,
                        Operation = operation,
                        Type = ErrorType.Fatal
                    };
                }
            }
            else
            {
                error = new Error
                {
                    Code = "100",
                    Description = Environment == Environments.Prod ? GenericError : baseEx.ToString(),
                    Message = Environment == Environments.Prod ? GenericError : baseEx.Message,
                    Module = module,
                    Operation = operation,
                    Type = ErrorType.Fatal
                };
            }

            return new List<Error> { error };
        }

        public static List<Error> ToErrors(this IEnumerable<ValidationResult> validationErrors, string moduleName = null, string operationName = null)
        {
            return validationErrors?.Select(e => new Error
            {
                //Code = "<SOME GENERIC CODE FOR VALIDATION>",
                Code = "VALIDATION",
                Description = string.Join(", ", e.MemberNames) + " : " + e.ErrorMessage,
                Message = e.ErrorMessage,
                Module = moduleName,
                Operation = operationName
            })?.ToList() ?? new List<Error>();
        }

        public static string GetConsolidatedMessage(this IEnumerable<ValidationResult> validationErrors)
        {
            return string.Join("; ", validationErrors.Select(e => e.ErrorMessage));
        }

        public static string EncrytBase64(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string DecryptBase64(this string cipherText)
        {
            var base64EncodedBytes = Convert.FromBase64String(cipherText);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static DataTable ToDataTable<T>(this List<T> items, List<string> columns = null)
        {
            columns = columns ?? new List<string>();
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] props;
            if (!columns.Any())
            {
                props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => !columns.Contains(p.Name)).ToArray();
            }
            else
            {
                var allProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToDictionary(p => p.Name, p => p, StringComparer.InvariantCultureIgnoreCase);
                props = columns.Where(c => allProps.ContainsKey(c)).Select(c => allProps[c]).ToArray();
            }

            foreach (PropertyInfo prop in props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static IDictionary<string, string> ToDictionary(this NameValueCollection col, IEqualityComparer<string> comparer = null)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>(comparer ?? StringComparer.InvariantCulture);
            foreach (var k in col.AllKeys)
            {
                dict.Add(k, col[k]);
            }
            return dict;
        }

        public static bool IsProd(this Environments environment)
        {
            return environment == Environments.Prod;
        }
    }
}
