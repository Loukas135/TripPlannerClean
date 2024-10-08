﻿using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Seeders.ServiceTypeSeeder
{
	internal class ServiceTypeSeeder(TripPlannerDbContext dbContext) : IServiceTypeSeeder
	{
		public async Task Seed()
		{
			if (dbContext.Database.CanConnect())
			{
				if (!dbContext.ServiceTypes.Any())
				{
					var serviceTypes = GetServiceTypes();
					dbContext.ServiceTypes.AddRange(serviceTypes);
					await dbContext.SaveChangesAsync();
				}
			}
		}

		private IEnumerable<ServiceType> GetServiceTypes()
		{
			List<ServiceType> serviceTypes = [
				new()
				{
					Name = "Hotel",
					ImagePath= "Images/ServiceTypes/Hotel.jpg"
                },
				new()
				{
					Name = "CarRental",
                    ImagePath= "Images/ServiceTypes/CarRental.jpg"
                },
				new()
				{
					Name = "TourismOffice",
                    ImagePath = "Images/ServiceTypes/TourismOffice.jpg"
                }
			];
			return serviceTypes;
		}
	}
}
