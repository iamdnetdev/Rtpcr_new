using log4net;
using log4net.Core;
using System;
using System.Data.SqlClient;

namespace RtpcrCustomerApp.Common.Logging
{
    public class Logger : LogImpl, ILog
    {
        public Logger(ILogger logger) : base(logger)
        {
        }

        public override void Error(object message, Exception exception)
        {
            var baseEx = exception.GetBaseException();
            if (baseEx.GetType() == typeof(SqlException))
            {
                var sqlEx = (SqlException)baseEx;
                // Handled errors from SP
                if (sqlEx.Number == 50001)
                {
                    base.Info(message + " : " + exception.Message);
                }
                else
                {
                    base.Error(message, exception);
                }
            }
            else
            {
                base.Error(message, exception);
            }
        }

    }
}
