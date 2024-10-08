﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
	public class ServiceRepository(TripPlannerDbContext dbContext,IRoomRepository roomRepository,
		ICarRepository carRepository,
		ITripRepository tripRepository,
		IHostEnvironment environment) : IServiceRepository
	{
		public async Task<int> Add(Service entity)
		{
			dbContext.Services.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task Delete(Service entity)
		{
			dbContext.Services.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Service>> Get()
		{
			var services = await dbContext.Services.ToListAsync();
			return services;
		}

		public async Task<Service?> GetById(int id)
		{
			var service = await dbContext.Services
			.Include(s => s.Rooms == null ? null : s.Rooms)
			.Include(s => s.Trips == null ? null : s.Trips)
			.Include(s => s.Cars == null ? null : s.Cars)
			.FirstOrDefaultAsync(x => x.Id == id);
			return service;
		}
		public async Task<Service?>GetByIdWithImages(int id)
		{
			var service = await dbContext.Services
			.Include(s => s.Rooms == null ? null : s.Rooms)
			.Include(s => s.Trips == null ? null : s.Trips)
			.Include(s => s.Cars == null ? null : s.Cars)
            .FirstOrDefaultAsync(x => x.Id == id);
            return service;
        }
        public async Task<Service?> GetByUserId(string ownerId)
		{
			var service = await dbContext.Services
			.Include(s => s.Rooms == null ? null : s.Rooms)
            .Include(s => s.Trips == null ? null : s.Trips)
            .Include(s => s.Cars == null ? null : s.Cars)
            .FirstOrDefaultAsync(x => x.OwnerId == ownerId);
			return service;
        }
        public async Task<Service?> GetByIdWithRating(int id)
        {
            var service = await dbContext.Services
            .Include(s => s.Ratings == null ? null : s.Ratings)
            .FirstOrDefaultAsync(x => x.Id == id);
            return service;
        }
		public async Task<float?>CalculateOverallRating(int id)
		{
			var service = await GetByIdWithRating(id);
			if (service == null)
			{
				return null;
			}

			List<Rate> ratings = service.Ratings.ToList();
			var overallRating = 0;
			foreach(var rating in ratings)
			{
				overallRating += (int) rating.Rating!;
			}

			overallRating /= ratings.Count;
			service.OverallRate = overallRating;

			dbContext.Services.Update(service);
			await dbContext.SaveChangesAsync();

			return overallRating;
		}
        public async Task SaveChanges() => await dbContext.SaveChangesAsync();

        public async Task<IEnumerable<Service>> GetServicesOfType(int governorateId, int serviceTypeId)
        {
			var servicesInGov = await dbContext.Services
				.Where(s => s.GovernorateId == governorateId).ToListAsync();
			if (servicesInGov == null)
			{
				return null;
			}
				var services= servicesInGov.Where(s => s.ServiceTypeId == serviceTypeId)
                .ToList();
            return services;
        }

		public async Task<Service> GetServiceWithReservations(int id)
		{
			var service = await dbContext.Services.Include(s => s.Reservations)
												  .FirstOrDefaultAsync(s => s.Id == id);
			return service;
		}

		public async Task<IEnumerable<Service>> GetServicesWithReservationsByGov(int id)
		{
			var servicesInGov = await dbContext.Services.Include(s => s.Reservations)
				.Where(s => s.GovernorateId == id).ToListAsync();

			return servicesInGov;
		}
		public async Task<IEnumerable<Service>>GetServiceFromUserReservation(string userid)
		{
			var services = from s in dbContext.Services
						   join r in dbContext.Reservations
							  on s.Id equals r.ServiceId
						   where r.UserId == userid
						   select s;
			return services;
		}
		public async Task FullyDeleteService(int id)
		{
			
			var service = await dbContext.Services
			.Include(s => s.Rooms == null ? null : s.Rooms)
			.Include(s => s.Trips == null ? null : s.Trips)
			.Include(s => s.Cars == null ? null : s.Cars)
			.Include(s=> s.Reservations == null ? null : s.Reservations)
			.FirstOrDefaultAsync(s => s.Id == id);
			if (service.ImagePath != null)
			{
				var path=Path.Combine(environment.ContentRootPath,service.ImagePath);
				File.Delete(path);
			}
			if (service.Rooms != null)
			{
                foreach (var room in service.Rooms)
                {
					if (room.ImagePath == null)
					{
						continue;
					}
                    var path = Path.Combine(environment.ContentRootPath, room.ImagePath);
                    File.Delete(path);
                    
                }
                dbContext.RemoveRange(service.Rooms);
            }
			if (service.Trips != null)
			{
                foreach (var trip in service.Trips)
                {
					if (trip.ImagePath == null)
					{
						continue;
					}
                    var path = Path.Combine(environment.ContentRootPath, trip.ImagePath);
                    File.Delete(path);
                }
                dbContext.RemoveRange(service.Trips);
            }
			if (service.Cars != null)
			{
                foreach (var car in service.Cars)
                {
					if(car.ImagePath == null)
					{
						continue;
					}
                    var path = Path.Combine(environment.ContentRootPath, car.ImagePath);
                    File.Delete(path);
                }
                dbContext.RemoveRange(service.Cars);
            }
            
            dbContext.RemoveRange(service.Reservations);
			dbContext.Remove(service);
			await dbContext.SaveChangesAsync();	
        }
		public async Task<IEnumerable<Reservation>> PaidReservations(int id,int year,int month)
		{
			var service = await dbContext.Services.Include(s => s.Reservations == null ? null
			: s.Reservations).FirstOrDefaultAsync(s => s.Id == id);
			if (service == null)
			{
				return null;
			}
			var reservations = service.Reservations.Where(r => r.Status == "Paid")
				.Where(r => r.From.Year == year && r.From.Month == month)
				.ToList();
			return reservations;

		}
		public async Task<int>PaidReservationsSum(int id,int year,int month)
		{
            var service = await dbContext.Services.Include(s => s.Reservations == null ? null
            : s.Reservations).FirstOrDefaultAsync(s => s.Id == id);
            if (service == null)
            {
                return 0;
            }
			var reservations = service.Reservations.Where(r => r.Status == "Paid")
				.Where(r => r.From.Year == year && r.From.Month == month).ToList();
			int sum = 0;
			foreach (var reservation in reservations)
			{
				sum += reservation.Cost;
			}
			return sum;
        }
        public async Task<string> SaveServiceImageAsync(IFormFile serviceImage)
        {
            if (serviceImage == null)
                return null;

            var contentPath = environment.ContentRootPath;
			string specialPath = "Images/Services";
            var path = Path.Combine(contentPath, specialPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var extension = Path.GetExtension(serviceImage.FileName);
            var fileName = $"{Guid.NewGuid().ToString()}{extension}";
            var fullName = Path.Combine(path, fileName);
			var returnName = Path.Combine(specialPath, fileName);
            using var stream = new FileStream(fullName, FileMode.Create);
            await serviceImage.CopyToAsync(stream);
            //var pathName = fullName.Substring(0);
            return returnName;
        }
		public async Task UpdateService(IFormFile newImage,Service service) {
			var path = await SaveServiceImageAsync(newImage);
			service.ImagePath=path;
			dbContext.Services.Update(service);
		}
    }
}
