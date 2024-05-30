namespace RtpcrCustomerApp.Common.Interfaces
{
    using log4net;
    using System;
    using System.Collections.Generic;

    public interface ILoggerFactory
    {
        ILog GetLogger<T>();
        ILog GetLogger(Type type);
    }
}
