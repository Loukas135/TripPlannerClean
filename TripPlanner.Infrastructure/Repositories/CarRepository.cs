using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
	public class CarRepository(TripPlannerDbContext dbContext) : ICarRepository
	{
		public async Task<int> Add(Car entity)
		{
			dbContext.Cars.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task Delete(Car entity)
		{
			dbContext.Cars.Remove(entity);
			await dbContext.SaveChangesAsync();
		}
	}
}
