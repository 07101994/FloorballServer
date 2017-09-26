using DAL.Ninject;
using DAL.Repository;
using Newtonsoft.Json;
using Ninject;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingService.Jobs
{
    public class MatchWillStartJob : BaseJob
    {
        public override async void Execute(IJobExecutionContext context)
        {
            base.Equals(context);

            var match = UoW.MatchRepository.GetMatchById(context.MergedJobDataMap.GetInt("matchId"));

            await Messanger.Instance.SendMatchWillStart(
                JsonConvert.SerializeObject(match),
                NotificationHelper.GetMatchWillStartTitleArgs(match),
                NotificationHelper.GetMatchWillStartBodyArgs(match),
                "matchStart_1"
                );

        }
    }
}
