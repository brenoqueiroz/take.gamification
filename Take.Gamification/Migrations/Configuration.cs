namespace Take.Gamification.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Take.Gamification.Models.GameContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Take.Gamification.Models.GameContext context)
        {
            context.Merits.AddOrUpdate(x => x.Name, new Merit
            {
                IsVisible = true,
                Name = "Comprometida",
                Value = 10
            });

            context.Merits.AddOrUpdate(x => x.Name, new Merit
            {
                IsVisible = false,
                Name = "Primeiro acesso",
                Value = 1
            });

            context.SaveChanges();
        }
    }
}
