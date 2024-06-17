using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
	public class RoomRepository(TripPlannerDbContext dbContext) : IRoomRepository
	{
		public async Task<int> Add(Room entity)
		{
			dbContext.Rooms.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task Delete(Room entity)
		{
			dbContext.Rooms.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task<Room?> GetById(int id)
		{
			var room = await dbContext.Rooms
				.Include(r => r.Reservations)
				.FirstOrDefaultAsync(r => r.Id == id);
			return room;
		}

		public async Task SaveChanges() => await dbContext.SaveChangesAsync();
	}
}
