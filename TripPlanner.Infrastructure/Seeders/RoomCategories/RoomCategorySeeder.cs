using TripPlanner.Domain.Entities.Service_Entities.Hotel;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Seeders.RoomCategories
{
	internal class RoomCategorySeeder(TripPlannerDbContext dbContext) : IRoomCategorySeeder
	{
		public async Task Seed()
		{
			if (await dbContext.Database.CanConnectAsync())
			{
				if (!dbContext.RoomCategories.Any())
				{
					var roomCategories = GetRoomCategories();
					dbContext.RoomCategories.AddRange(roomCategories);
					await dbContext.SaveChangesAsync();
				}
			}
		}

		private IEnumerable<RoomCategory> GetRoomCategories()
		{
			List<RoomCategory> roomCategories = [
				new()
				{
					Name = "First Class"
				},
				new()
				{
					Name = "Second Class"
				},
				new()
				{
					Name = "Third Class"
				}
			];
			return roomCategories;
		}
	}
}
