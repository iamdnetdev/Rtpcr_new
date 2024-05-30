namespace RtpcrCustomerApp.Repositories.Common
{
    using Dapper;
    using Dapper.Contrib.Extensions;
    using Interfaces;
    using log4net;
    using RtpcrCustomerApp.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;

    public abstract class RepositoryBase : IRepository, IDisposable
    {
        private static readonly Dictionary<string, List<PropertyInfo>> WritablePropertiesCache = new Dictionary<string, List<PropertyInfo>>();
        protected IDbSetting Settings;
        protected readonly ILog logger;
        private SqlConnection m_connection;
        public RepositoryBase(IDbSetting settings, ILog logger)
        {
            Settings = settings;
            this.logger = logger;
        }

        public SqlConnection GetConnection()
        {
            m_connection = new SqlConnection(Settings.ConnectionString);
            m_connection.Open();
            return m_connection;
        }

        public int ExecuteCommand<T>(string sql, T parameter, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var conn = GetConnection())
            {
                var dynamicParameter = BuildDynamicParameters(parameter);
                return conn.Execute(sql, param: dynamicParameter, commandType: commandType);
            }
        }

        public int ExecuteCommand(string sql, CommandType commandType = CommandType.StoredProcedure, params KeyValuePair<string, object>[] parameters)
        {
            using (var conn = GetConnection())
            {
                using (var cmd = BuildCommand(sql, conn, parameters))
                {
                    cmd.CommandType = commandType;
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public SqlDataReader ExecuteReader<T>(string sql, T parameter, CommandType commandType = CommandType.StoredProcedure)
        {
            var conn = GetConnection();
            return (SqlDataReader)conn.ExecuteReader(sql, param: parameter, commandType: commandType);
        }

        public SqlDataReader ExecuteReader(string sql, CommandType commandType = CommandType.StoredProcedure, params KeyValuePair<string, object>[] parameters)
        {
            var conn = GetConnection();
            var cmd = BuildCommand(sql, conn, parameters);
            cmd.CommandType = commandType;
            return (SqlDataReader)cmd.ExecuteReader();
        }

        public IEnumerable<R> Query<T, R>(string sql, T parameter, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var conn = GetConnection())
            {
                return conn.Query<R>(sql, param: parameter, commandType: commandType);
            }
        }

        public IEnumerable<R> Query<R>(string sql, CommandType commandType = CommandType.StoredProcedure, params KeyValuePair<string, object>[] parameters)
        {
            using (var conn = GetConnection())
            {
                var cmdDefn = new CommandDefinition(sql);
                var objParam = new ExpandoObject();
                var eoColl = (ICollection<KeyValuePair<string, object>>)objParam;
                foreach (var kvp in parameters)
                {
                    eoColl.Add(kvp);
                }
                dynamic eoDynamic = objParam;

                return conn.Query<R>(sql, param: (object)eoDynamic, commandType: commandType);
            }
        }

        public R ExecuteScalar<T, R>(string sql, T parameter, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var conn = GetConnection())
            {
                return conn.ExecuteScalar<R>(sql, 
                    param: typeof(T) == typeof(object) ? (object)parameter : (object)BuildDynamicParameters(parameter), 
                    commandType: commandType);
            }
        }

        public R ExecuteScalar<R>(string sql, CommandType commandType = CommandType.StoredProcedure, params KeyValuePair<string, object>[] parameters)
        {
            using (var conn = GetConnection())
            {
                using (var cmd = BuildCommand(sql, conn, parameters))
                {
                    cmd.CommandType = commandType;
                    return (R)cmd.ExecuteScalar();
                }
            }
        }

        public void Dispose()
        {
            if (m_connection != null && m_connection.State != ConnectionState.Closed)
            {
                try { m_connection.Close(); } catch { }
            }
        }

        private SqlCommand BuildCommand(string sql, IDbConnection conn, params KeyValuePair<string, object>[] parameters)
        {
            var sqlCmd = new SqlCommand(sql, (SqlConnection)conn);
            foreach (var param in parameters)
            {
                sqlCmd.Parameters.AddWithValue(param.Key, param.Value);
            }
            return sqlCmd;
        }

        private DynamicParameters BuildDynamicParameters<T>(T parameter)
        {
            var writableProperties = GetWritableProperties(typeof(T));
            var parameters = new DynamicParameters();
            foreach (var prop in writableProperties)
            {
                if (prop.Name == "TestUserOrders")
                {
                    //var parameters1 = new DynamicParameters();
                    //parameters1.AddTable("@sprocParameterName", "[dbo].[IntList]", intList);

                    DataTable TestUserOrder = new DataTable();
                    TestUserOrder.Columns.Add("UserID", typeof(Guid));
                    TestUserOrder.Columns.Add("ProductId", typeof(Guid));
                    DataRow dr = null;
                    dynamic data = prop.GetValue(parameter);
                    foreach (var value in data)
                    {
                        dr = TestUserOrder.NewRow();
                        dr["UserID"] = value.UserID;
                        dr["ProductId"] = value.ProductId;
                        TestUserOrder.Rows.Add(dr);
                    }
                    var TestUserOrders = TestUserOrder.AsTableValuedParameter("[dbo].[TestUserOrder]");
                    parameters.Add("@TestUserOrders", TestUserOrders);
                    //parameters.Add(prop.Name, prop.GetValue(TestUserOrders));
                }
                else
                    parameters.Add(prop.Name, prop.GetValue(parameter));
            }
            return parameters;
        }

        private List<PropertyInfo> GetWritableProperties(Type type)
        {
            if (WritablePropertiesCache.TryGetValue(type.FullName, out List<PropertyInfo> props)) return props;
            var writableProps = type.GetProperties().Where(p =>
            {
                var attrs = p.GetCustomAttributes(typeof(WriteAttribute), true);
                if (attrs != null && attrs.Any())
                {
                    return ((WriteAttribute)attrs.First()).Write;
                }
                return true;
            }).ToList();
            WritablePropertiesCache.Add(type.FullName, writableProps);
            return writableProps;
        }
    }
}
