using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Tourism_Office;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
	public class TripRepository(TripPlannerDbContext dbContext) : ITripRepository
	{
		public async Task<int> Add(Trip entity)
		{
			dbContext.Trips.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task Delete(Trip entity)
		{
			dbContext.Trips.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task<Trip?> GetById(int id)
		{
			var trip = await dbContext.Trips.FirstOrDefaultAsync(t => t.Id == id);
			return trip;
		}
	}
}
