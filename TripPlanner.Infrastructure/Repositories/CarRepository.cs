using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;
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
        public async Task DeleteCarReservations(int id)
        {
            var car = await dbContext.Cars.Include(r => r.Reservations)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (car== null || car.Reservations == null)
            {
                return;
            }
            dbContext.Reservations.RemoveRange(car.Reservations);
            await dbContext.SaveChangesAsync();
        }
        public async Task<string> SaveCarImageAsync(IFormFile carImage)
        {
            if (carImage == null)
                return null;
            var contentPath = environment.ContentRootPath;
            var specialPath = "Images/Cars";
            var path = Path.Combine(contentPath,specialPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var extension = Path.GetExtension(carImage.FileName);
            var fileName = $"{Guid.NewGuid().ToString()}{extension}";
            var fullName = Path.Combine(path, fileName);
            var returnName = Path.Combine(specialPath, fileName);
            using var stream = new FileStream(fullName, FileMode.Create);
            await carImage.CopyToAsync(stream);
            return returnName;
        }

        public async Task FullyDeleteCar(int id)
        {
            var car = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
            {
                return;
            }
            if (car.ImagePath != null)
            {
                var path = Path.Combine(environment.ContentRootPath, car.ImagePath);
                File.Delete(path);
            }
            await DeleteCarReservations(id);
            await Delete(car);
        }

		public async Task UpdateCar(IFormFile newImage, Car car)
		{
            var content = environment.ContentRootPath;
            if (car.ImagePath != null) {
				var deletePath = Path.Combine(content, car.ImagePath);
				File.Delete(deletePath);
			}
			var path = await SaveCarImageAsync(newImage);
			car.ImagePath = path;
			dbContext.Cars.Update(car);
            await dbContext.SaveChangesAsync();
		}
	}
}
