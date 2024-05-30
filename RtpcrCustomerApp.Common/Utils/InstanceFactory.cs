namespace RtpcrCustomerApp.Common.Utils
{
    using Autofac;
    using System;

    public static class InstanceFactory
    {
        public static IContainer Container { get; set; }

        public static T GetInstance<T>()
        {
            try
            {
                return Container.Resolve<T>();
            }
            catch
            {
                return default(T);
            }
        }

        public static object GetInstance(Type type)
        {
            return Container.Resolve(type);
        }
    }
}
