using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
    public class ServicetypeRepository : SeededValuesRepository<ServiceType>, IServicetypeRepository
    {
        public ServicetypeRepository(TripPlannerDbContext dbContext):base(dbContext)
        {
        }
    }
}
