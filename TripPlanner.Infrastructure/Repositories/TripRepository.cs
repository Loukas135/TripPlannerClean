﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
			var path = Path.Combine(contentPath, "Images/Trips");
			if(Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			var extension = Path.GetExtension(tripImage.FileName);
			var imageName = $"{Guid.NewGuid().ToString()}{extension}";
			var fullName = Path.Combine(path, imageName);
			using var stream = new FileStream(fullName, FileMode.Create);
			await stream.CopyToAsync(stream);
			return fullName;
		}
	}
}
