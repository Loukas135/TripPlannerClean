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
                    Description = "Damascus is the capital of Syria",
                    ImagePath = "Images/Governorates/Damascus.jpg"
                },
                new()
                {
                    Name = "Latakia",
                    Description = "Latakia is one of the Syrian governorate",
                    ImagePath = "Images/Governorates/Latakia.jpg"
                },
                new()
                {
                    Name = "Tartous",
                    Description = "Tartous is one of the Syrian governorate",
                    ImagePath = "Imgaes/Governorates/Tartous.jpg"
                },
                new()
                {
                    Name = "Aleppo",
                    Description = "Aleppo is one of the Syrian governorate",
                    ImagePath = "Images/Governorates/Aleppo.jpg"
                },
                new()
                {
                    Name = "Daraa",
                    Description = "Daraa is one of the Syrian governorate",
                    ImagePath = "Images/Governorates/Daraa.jpg"
                },
                new()
                {
                    Name = "Qunaitira",
                    Description = "Qunaitira is one of the Syrian governorate",
                   ImagePath = "Images/Governorates/Qunaitira.jpg"
                },
                new()
                {
                    Name = "Sweida",
                    Description = "Sweida is one of the Syrian governorate",
                    ImagePath = "Images/Governorates/Sweida.jpg"
                },
                new()
                {

                    Name = "Idleb",
                    Description = "Idleb is one of the Syrian governorate",
                    ImagePath = "Images/Governorates/Idleb.jpg"
                },
                new()
                {
                    Name = "Deir Al Zor",
                    Description = "Deir Al Zor is one of the Syrian governorate",
                    ImagePath = "Images/Governorates/Deir_Al_Zor.jpg"
                },
                new()
                {
                    Name = "Al Hasaka",
                    Description = "Al Hasaka is one of the Syrian governorate",
                    ImagePath = "Images/Governorates/Al_Hasaka.jpg"
                },
                new()
                {
                    Name = "Homs",
                    Description = "Homs is one of the Syrian governorate",
                    ImagePath = "Images/Governorates/Homs.jpg"
                },
                new()
                {
                    Name = "Al Raqqa",
                    Description = "Al Raqqa is one of the Syrian governorate",
                     ImagePath = "Images/Governorates/Al_Raqqa.jpg"
                },
                new()
                {
                    Name = "Hama",
                    Description = "Hama is one of the Syrian governorate",
                     ImagePath = "Images/Governorates/Hama.jpg"
                },
                new()
                {
                    Name = "Liwaa Iskandaron",
                    Description = "Liwaa Iskandaron is one of the Syrian governorate",
                     ImagePath = "Images/Governorates/Liwaa_Iskandaron.jpg"
                },
            ];
            return governorates;
        }
    }
}
