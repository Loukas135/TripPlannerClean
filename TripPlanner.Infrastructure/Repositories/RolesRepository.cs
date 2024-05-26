using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
    public class RolesRepository:SeededValuesRepository<IdentityRole>,IRolesRepository
    {
        public RolesRepository(TripPlannerDbContext dbContext):base(dbContext)
        {
            
        }
    }
}
