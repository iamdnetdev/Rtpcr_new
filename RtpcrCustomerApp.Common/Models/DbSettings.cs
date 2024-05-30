using RtpcrCustomerApp.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtpcrCustomerApp.Common.Models
{
    public class DbSettings : IDbSetting
    {
        public DbSettings()
        {

        }
        public string Name { get; set; } 
        public string ConnectionString { get; set; }
        public string SchemaName { get; set; } 
    }
}
