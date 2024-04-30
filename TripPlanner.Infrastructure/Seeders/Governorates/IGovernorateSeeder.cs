using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Infrastructure.Seeders.Governorates
{
    public interface IGovernorateSeeder
    {
        public Task Seed();
    }
}
