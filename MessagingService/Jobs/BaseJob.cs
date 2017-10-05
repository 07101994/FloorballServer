using DAL.Ninject;
using DAL.Repository;
using Ninject;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingService.Jobs
{
    public class BaseJob : IJob
    {
        protected IUnitOfWork UoW { get; set; }

        public virtual void Execute(IJobExecutionContext context)
        {
            IKernel kernel = new StandardKernel(new Bindings());
            UoW = kernel.Get<IUnitOfWork>();
        }
    }
}
