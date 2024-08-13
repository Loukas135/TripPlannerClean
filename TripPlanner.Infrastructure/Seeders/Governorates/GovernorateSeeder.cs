using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Seeders.Governorates
{
    internal class GovernorateSeeder(TripPlannerDbContext dbContext) : IGovernorateSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Governorates.Any())
                {
                    var governorates = GetGovernorates();
                    dbContext.Governorates.AddRange(governorates);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Governorate> GetGovernorates()
        {
            List<Governorate> governorates = [
                new()
                {
                    Name = "Damascus",
                    Description = "Damascus is the capital of Syria"
                },
                new()
                {
                    Name = "Latakia",
                    Description = "Latakia is one of the Syrian governorate"
                },
                new()
                {
                    Name = "Tartous",
                    Description = "Tartous is one of the Syrian governorate"
                },
                new()
                {
                    Name = "Aleppo",
                    Description = "Aleppo is one of the Syrian governorate"
                },
                new()
                {
                    Name = "Daraa",
                    Description = "Daraa is one of the Syrian governorate"
                },
                new()
                {
                    Name = "Qunaitira",
                    Description = "Qunaitira is one of the Syrian governorate"
                },
                new()
                {
                    Name = "Sweida",
                    Description = "Sweida is one of the Syrian governorate"
                },
                new()
                {

                    Name = "Idleb",
                    Description = "Idleb is one of the Syrian governorate"
                },
                new()
                {
                    Name = "Deir Al Zor",
                    Description = "Deir Al Zor is one of the Syrian governorate"
                },
                new()
                {
                    Name = "Al Hasaka",
                    Description = "Al Hasaka is one of the Syrian governorate"
                },
                new()
                {
                    Name = "Homs",
                    Description = "Homs is one of the Syrian governorate"
                },
                new()
                {
                    Name = "Al Raqqa",
                    Description = "Al Raqqa is one of the Syrian governorate"
                },
                new()
                {
                    Name = "Hama",
                    Description = "Hama is one of the Syrian governorate"
                },
                new()
                {
                    Name = "Liwaa Iskandaron",
                    Description = "Liwaa Iskandaron is one of the Syrian governorate"
                },
            ];
            return governorates;
        }
    }
}
