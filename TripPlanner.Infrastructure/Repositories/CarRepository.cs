using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
	public class CarRepository(TripPlannerDbContext dbContext,IHostEnvironment environment) : ICarRepository
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

		public async Task<Car?> GetById(int id)
		{
			var car = await dbContext.Cars.Include(c => c.Reservations)
										  .FirstOrDefaultAsync(c => c.Id == id);
			return car;
		}

		public async Task SaveChanges() => await dbContext.SaveChangesAsync();

        public async Task<string> SaveCarImageAsync(IFormFile carImage)
        {
            if (carImage == null)
                return null;
            var contentPath = environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Images/Cars");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var extension = Path.GetExtension(carImage.FileName);
            var fileName = $"{Guid.NewGuid().ToString()}{extension}";
            var fullName = Path.Combine(path, fileName);
            using var stream = new FileStream(fullName, FileMode.Create);
            await carImage.CopyToAsync(stream);
            return fullName;
        }
    }
}
