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
    public class RatingRepository(TripPlannerDbContext dbContext,IServiceRepository serviceRepository) : IRatingRepository
    {
        public async Task<int> AddRating(Rate entity)
        {
            await dbContext.Ratings.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteRating(Rate entity)
        {
            var id = entity.ServiceId;
            dbContext.Ratings.Remove(entity);
            await dbContext.SaveChangesAsync();
            await serviceRepository.CalculateOverallRating(id);
        }

        public async Task<IEnumerable<Rate>> GetRatingsOfUser(string userId)
        {
            var ratings = await dbContext.Ratings.Where(r => r.UserId == userId).ToListAsync();
            return ratings;
            
        }

        public async Task UpdateRating(Rate entity)
        {
            var id = entity.ServiceId;
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
           await  serviceRepository.CalculateOverallRating(id);
        }
    }
}
