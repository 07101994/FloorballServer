using Bll.Seed;
using DAL.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.DBInitializer
{
    public class CreateDatabaseIfNotExistsInitializer : CreateDatabaseIfNotExists<FloorballBaseCtx>
    {

        [Inject]
        public ISeeder Seeder { get; set; }

        protected override void Seed(FloorballBaseCtx context)
        {
            base.Seed(context);

            Seeder.Seed(context);

        }



    }
}
