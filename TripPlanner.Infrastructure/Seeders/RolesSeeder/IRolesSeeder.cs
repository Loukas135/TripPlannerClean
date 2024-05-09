using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Infrastructure.Seeders.RolesSeeder
{
    public interface IRolesSeeder
    {
        Task Seed();
    }
}
