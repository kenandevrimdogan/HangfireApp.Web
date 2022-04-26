using Hangfire;
using System.Diagnostics;

namespace HangfireApp.Web.BackgroundJobs
{
    public class RecurringJobs
    {
        public static void ReportingJobs()
        {
            Hangfire.RecurringJob.AddOrUpdate("reportJob", () => EmailReport(), Cron.Minutely());
        }

        public static void EmailReport()
        {
            Debug.WriteLine("Report sent with email");
        }

    }
}
