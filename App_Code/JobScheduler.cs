using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartZExample
{
    public class JobScheduler
    {
        /// <summary>
        /// This function will start when application up and runing and schedule cron job for email
        /// </summary>
        public static void Start()
        {
            IJobDetail job = JobBuilder.Create<EmailJob>()
                                  .WithIdentity("job1")
                                  .Build();

            ITrigger trigger = TriggerBuilder.Create()
                                            .WithDailyTimeIntervalSchedule
                                              (s =>
                                                 s.WithIntervalInSeconds(30)
                                                .OnEveryDay()
                                              )
                                             .ForJob(job)
                                             .WithIdentity("trigger1")
                                             .StartNow()
                                             .WithCronSchedule("0 47 18 ? * MON-FRI *")
                                             .Build();

            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sc = sf.GetScheduler();
            sc.ScheduleJob(job, trigger);
            sc.Start();
        }
    }
}