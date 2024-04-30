using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Seeders.CarCategories
{
	internal class CarCategorySeeder(TripPlannerDbContext dbContext) : ICarCategorySeeder
	{
		public async Task Seed()
		{
			if (await dbContext.Database.CanConnectAsync())
			{
				if (!dbContext.CarCategories.Any())
				{
					var carCategories = GetCarCategories();
					dbContext.CarCategories.AddRange(carCategories);
					await dbContext.SaveChangesAsync();
				}
			}
		}

		private IEnumerable<CarCategory> GetCarCategories()
		{
			List<CarCategory> carCategories = [
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
			return carCategories;
		}
	}
}
