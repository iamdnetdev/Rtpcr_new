namespace RtpcrCustomerApp.Repositories.Interfaces
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public interface IRepository
    {
        int ExecuteCommand(string sql, CommandType commandType = CommandType.StoredProcedure, params KeyValuePair<string, object>[] parameters);
        int ExecuteCommand<T>(string sql, T parameter, CommandType commandType = CommandType.StoredProcedure);
        SqlDataReader ExecuteReader(string sql, CommandType commandType = CommandType.StoredProcedure, params KeyValuePair<string, object>[] parameters);
        SqlDataReader ExecuteReader<T>(string sql, T parameter, CommandType commandType = CommandType.StoredProcedure);
        R ExecuteScalar<R>(string sql, CommandType commandType = CommandType.StoredProcedure, params KeyValuePair<string, object>[] parameters);
        R ExecuteScalar<T, R>(string sql, T parameter, CommandType commandType = CommandType.StoredProcedure);
        SqlConnection GetConnection();
        IEnumerable<R> Query<R>(string sql, CommandType commandType = CommandType.StoredProcedure, params KeyValuePair<string, object>[] parameters);
        IEnumerable<R> Query<T, R>(string sql, T parameter, CommandType commandType = CommandType.StoredProcedure);
    }
}
