using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Seeders.RolesSeeder
{
    public class RolesSeeder(TripPlannerDbContext dbContext):IRolesSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Roles.Any()) { 
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
        }
        public IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =[
                new ()
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new ()
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new ()
                {
                    Name = "HotelOwner",
                    NormalizedName = "HOTELOWNER"
                },
                new ()
                {
                    Name = "CarRental",
                    NormalizedName = "CARRENTAL"
                },
                new ()
                {
                    Name = "TourismOffice",
                    NormalizedName = "TOURISMOFFICE"
                },
                new ()
                {
                    Name = "Restaurant",
                    NormalizedName = "RESTAURANT"
                }
                ];
            return roles;
        }
    }
}
