namespace RtpcrCustomerApp.Common.Logging
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttribute : Attribute
    {
        public string Name { get; set; }
        public ModuleAttribute(string name)
        {
            Name = name;
        }
    }
}
