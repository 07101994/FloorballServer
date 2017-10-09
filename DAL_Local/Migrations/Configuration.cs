namespace DAL_Local.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LocalContext>
    {
        

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LocalContext context)
        {
            context.Seeder.Seed(context);
        }
    }
}
