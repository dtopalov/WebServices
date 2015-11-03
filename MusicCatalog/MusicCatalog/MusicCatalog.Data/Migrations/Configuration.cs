namespace MusicCatalog.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using MusicCatalog.Data.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MusicCatalog.Data.MusicCatalogDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MusicCatalog.Data.MusicCatalogDbContext context)
        {
            //This method will be called after migrating to the latest version.

            //You can use the DbSet<T>.AddOrUpdate() helper extension method
            //to avoid creating duplicate seed data.E.g.

              context.Artists.AddOrUpdate(
                p => p.Name,
                new Artist { Name = "Andrew Peters" },
                new Artist { Name = "Brice Lambson" },
                new Artist { Name = "Rowan Miller" }
              );

        }
    }
}
