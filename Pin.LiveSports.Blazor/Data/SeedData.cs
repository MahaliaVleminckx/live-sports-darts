using Pin.LiveSports.Core.Models;

namespace Pin.LiveSports.Blazor.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Countries.Any() || context.Players.Any())
            {
                return;
            }
              

            // COUNTRIES
            var belgium = new Country { Name = "Belgium" };
            var netherlands = new Country { Name = "Netherlands" };
            var england = new Country { Name = "England" };
            var germany = new Country { Name = "Germany" };

            context.Countries.AddRange(belgium, netherlands, england, germany);

            context.SaveChanges();

            // PLAYERS
            context.Players.AddRange(
                new Player { Name = "Dimitri", Nickname = "The DreamMaker", CountryId = belgium.Id },
                new Player { Name = "Kim", Nickname = "Hurricane", CountryId = belgium.Id },

                new Player { Name = "Michael", Nickname = "Mighty Mike", CountryId = netherlands.Id },
                new Player { Name = "Dirk", Nickname = "Aubergenius", CountryId = netherlands.Id },

                new Player { Name = "Luke", Nickname = "The Nuke", CountryId = england.Id },
                new Player { Name = "Gerwyn", Nickname = "The Iceman", CountryId = england.Id }
            );

            context.SaveChanges();

        }
    }
}
