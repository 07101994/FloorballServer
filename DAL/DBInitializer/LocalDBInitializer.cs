using Bll.Context;
using Bll.Seed;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBInitializer
{
    public class LocalDBInitializer<T> where T : LocalCtx
    {
        [Inject]
        public IDatabaseInitializer<T> DBInitializer { get; set; }

        public class CreateDatabaseIfNotExistsInitializer : CreateDatabaseIfNotExists<T>
        {
            [Inject]
            public ISeeder Seeder { get; set; }

            protected override void Seed(T context)
            {
                base.Seed(context);

                Seeder.Seed(context);
                
            }
        }

        public class DropCreateDatabaseAlwaysInitializer : DropCreateDatabaseAlways<T>
        {
            [Inject]
            public ISeeder Seeder { get; set; }

            public override void InitializeDatabase(T context)
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

                base.InitializeDatabase(context);
            }

            protected override void Seed(T context)
            {
                base.Seed(context);

                Seeder.Seed(context);

            }
        }

        public class DropCreateDatabaseIfModelChangesInitializer : DropCreateDatabaseIfModelChanges<T>
        {
            [Inject]
            public ISeeder Seeder { get; set; }

            protected override void Seed(T context)
            {
                base.Seed(context);

                Seeder.Seed(context);

            }
        }

    }
}
