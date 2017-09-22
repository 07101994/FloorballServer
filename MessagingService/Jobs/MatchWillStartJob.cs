using DAL.Repository;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingService.Jobs
{
    public class MatchWillStartJob : IJob
    {
        public async void Execute(IJobExecutionContext context)
        {
            var UoW = new UnitOfWork(null);

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
