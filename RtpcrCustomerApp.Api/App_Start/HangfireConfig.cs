using Hangfire;
using Hangfire.SqlServer;
using Owin;
using RtpcrCustomerApp.Api.Jobs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RtpcrCustomerApp.Api.App_Start
{
    public class HangfireConfig
    {
        public static void ConfigureJobs(IAppBuilder app)
        {
            app.UseHangfireAspNet(GetHangfireServers)
               .UseHangfireDashboard("/hangfire", new DashboardOptions
               {
                   DashboardTitle = "Hangfire Jobs"
               });
            RecurringJob.AddOrUpdate<VaccinationOrderJob>("Vaccine-ScheduledOrder-AssignVaccinator", t => t.Execute(), Cron.Minutely);
            RecurringJob.AddOrUpdate<TestOrderJob>("Test-ScheduledOrder-AssignCollector", t => t.Execute(), Cron.Minutely);
        }

        private static IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseLog4NetLogProvider()
                .UseSqlServerStorage(ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });

            yield return new BackgroundJobServer();
        }

    }
}