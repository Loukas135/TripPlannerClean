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
    public class ServiceImageRepository(TripPlannerDbContext dbContext): IServiceImageRepository
    {
        public async Task<int> addServiceImageAsync(ServiceImage serviceImage)
        {
           await dbContext.ServiceImages.AddAsync(serviceImage);
          await  dbContext.SaveChangesAsync();
            return serviceImage.Id;
        }
    }
}
