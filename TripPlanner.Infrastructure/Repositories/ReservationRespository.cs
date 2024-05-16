using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
	public class ReservationRespository(TripPlannerDbContext dbContext) : IReservationRespository
	{
		public async Task<int> Add(Reservation entity)
		{
			dbContext.Reservations.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}
	}
}
