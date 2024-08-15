using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
	public class RoomRepository(TripPlannerDbContext dbContext,IHostEnvironment environment) : IRoomRepository
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
        public async Task DeleteRoomReservations(int id)
        {
            var room = await dbContext.Rooms.Include(r => r.Reservations)
                .FirstOrDefaultAsync(r => r.Id == id);
            if(room==null || room.Reservations == null)
            {
                return;
            }
            dbContext.Reservations.RemoveRange(room.Reservations);
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<string> SaveRoomImageAsync(IFormFile roomImage)
        {
            if (roomImage == null)
                return null;

            var contentPath = environment.ContentRootPath;
            var specialPath = "Images/Rooms";
            var path = Path.Combine(contentPath, specialPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var extension = Path.GetExtension(roomImage.FileName);
            var fileName = $"{Guid.NewGuid().ToString()}{extension}";
            var fullName = Path.Combine(path, fileName);
            var returnName = Path.Combine(specialPath, fileName);
            using var stream = new FileStream(fullName, FileMode.Create);
			await roomImage.CopyToAsync(stream);
           // var pathName = fullName.Substring(0);
            return returnName;
        }

        public async Task FullyDeleteRoom(int id)
        {
            var room = await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if(room==null) { return;}
            await DeleteRoomReservations(id);
            if (room.ImagePath != null)
            {
                var path = Path.Combine(environment.ContentRootPath, room.ImagePath);
                File.Delete(path);
            }
            await Delete(room);


        }
    }
}
