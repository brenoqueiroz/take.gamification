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
                Name = MeritsConst.Comprometimento,
                Value = 10
            });

            context.Merits.AddOrUpdate(x => x.Name, new Merit
            {
                IsVisible = true,
                Name = MeritsConst.RespeitoPessoas,
                Value = 10
            });

            context.Merits.AddOrUpdate(x => x.Name, new Merit
            {
                IsVisible = true,
                Name = MeritsConst.QualidadeTrabalho,
                Value = 7
            });

            context.Merits.AddOrUpdate(x => x.Name, new Merit
            {
                IsVisible = true,
                Name = MeritsConst.Solicito,
                Value = 5
            });

            context.Merits.AddOrUpdate(x => x.Name, new Merit
            {
                IsVisible = false,
                Name = MeritsConst.PrimeiroAcesso,
                Value = 10
            });

            context.Merits.AddOrUpdate(x => x.Name, new Merit
            {
                IsVisible = false,
                Name = MeritsConst.CadastrarFoto,
                Value = 10
            });

            context.Merits.AddOrUpdate(x => x.Name, new Merit
            {
                IsVisible = false,
                Name = MeritsConst.ConcederMerito,
                Value = 1
            });

            context.Merits.AddOrUpdate(x => x.Name, new Merit
            {
                IsVisible = false,
                Name = MeritsConst.PrimeiroAcessoDia,
                Value = 1
            });
            context.SaveChanges();
        }
    }
}
