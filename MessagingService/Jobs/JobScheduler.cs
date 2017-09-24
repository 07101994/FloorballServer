﻿using DAL.Repository;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingService.Jobs
{
    public class JobScheduler
    {

        public UnitOfWork UoW { get; set; }

        public JobScheduler()
        {
            UoW = new UnitOfWork();
        }


        public void Start(int hours)
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();

            foreach (var match in UoW.MatchRepository.GetAllMatch().Where(m => m.Date < DateTime.Now))
            {
                IJobDetail jobDetail = JobBuilder.Create<MatchWillStartJob>().Build();

                ITrigger jobTrigger = TriggerBuilder.Create()
                    .WithIdentity(match.Id.ToString(), "MatchStart")
                    //.StartAt(match.Date.AddHours(hours))
                    .StartAt(DateTime.Now.AddSeconds(20))
                    .WithPriority(1)
                    .UsingJobData("matchId",match.Id)
                    .Build();

                scheduler.ScheduleJob(jobDetail, jobTrigger);

            }

        }

    }
}
