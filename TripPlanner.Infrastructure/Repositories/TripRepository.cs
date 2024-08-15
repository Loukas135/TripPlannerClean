using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;
using TripPlanner.Domain.Entities.Service_Entities.Tourism_Office;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
	public class TripRepository(TripPlannerDbContext dbContext, IHostEnvironment hostEnvironment) : ITripRepository
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

		public async Task SaveChanges() => await dbContext.SaveChangesAsync();

		public async Task<string> SaveTripImageAsync(IFormFile tripImage)
		{
            if (tripImage == null)
                return null;

            var contentPath = hostEnvironment.ContentRootPath;
            var specialPath = "Images/Trips";
            var path = Path.Combine(contentPath, specialPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var extension = Path.GetExtension(tripImage.FileName);
            var fileName = $"{Guid.NewGuid().ToString()}{extension}";
            var fullName = Path.Combine(path, fileName);
            var returnName = Path.Combine(specialPath, fileName);
            using var stream = new FileStream(fullName, FileMode.Create);
            await tripImage.CopyToAsync(stream);
            return returnName;
    }
        public async Task DeleteTripReservations(int id)
        {
            var trip = await dbContext.Trips.Include(r => r.Reservations)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (trip == null || trip.Reservations == null)
            {
                return;
            }
            dbContext.Reservations.RemoveRange(trip.Reservations);
            await dbContext.SaveChangesAsync();
        }

        public async  Task FullyDeleteTrip(int id)
        {
            var trip = await dbContext.Trips.FirstOrDefaultAsync(t => t.Id == id);
            if (trip == null)
            {
                return;
            }
            if (trip.ImagePath != null)
            {
                var path = Path.Combine(hostEnvironment.ContentRootPath, trip.ImagePath);
                File.Delete(path);
            }
            await DeleteTripReservations(id);
           await  Delete(trip);

        }
    }
}
