using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
	public class ServiceRepository(TripPlannerDbContext dbContext) : IServiceRepository
	{
		public async Task<int> Add(Service entity)
		{
			dbContext.Services.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task Delete(Service entity)
		{
			dbContext.Services.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Service>> Get()
		{
			var services = await dbContext.Services.ToListAsync();
			return services;
		}

		public async Task<Service?> GetById(int id)
		{
			var service = await dbContext.Services
			.Include(s => s.Rooms == null ? null : s.Rooms)
			.Include(s => s.Trips == null ? null : s.Trips)
			.Include(s => s.Cars == null ? null : s.Cars)
			.FirstOrDefaultAsync(x => x.Id == id);
			return service;
		}
        public async Task<Service?> GetByIdWithRating(int id)
        {
            var service = await dbContext.Services
            .Include(s => s.Ratings == null ? null : s.Ratings)
            .FirstOrDefaultAsync(x => x.Id == id);
            return service;
        }
		public async Task<float?>CalculateOverallRating(int id)
		{
			var service = await GetByIdWithRating(id);
			if (service == null)
			{
				return null;
			}

			List<Rate> ratings = service.Ratings.ToList();
			var overallRating = 0;
			foreach(var rating in ratings)
			{
				overallRating += (int) rating.Rating!;
			}

			overallRating /= ratings.Count;
			//service.Rate = overallRating;

			dbContext.Services.Update(service);
			await dbContext.SaveChangesAsync();

			return overallRating;
		}
        public async Task SaveChanges() => await dbContext.SaveChangesAsync();

        public async Task<IEnumerable<Service>> GetServicesOfType(int governorateId, int serviceTypeId)
        {
			var servicesInGov = await dbContext.Services
				.Where(s => s.GovernorateId == governorateId).ToListAsync();
			if (servicesInGov == null)
			{
				return null;
			}
				var services= servicesInGov.Where(s => s.ServiceTypeId == serviceTypeId)
                .ToList();
            return services;
        }
    }
}
