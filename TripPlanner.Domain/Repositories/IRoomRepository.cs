using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;

namespace TripPlanner.Domain.Repositories
{
	public interface IRoomRepository
	{
		public Task<int> Add(Room entity);
		public Task Delete(Room entity);
		public Task<Room?> GetById(int id);
		public Task SaveChanges();
		public Task DeleteRoomReservations(int id);
		public Task FullyDeleteRoom(int id);
        public Task<string> SaveRoomImageAsync(IFormFile roomImage);
		public Task UpdateRoom(IFormFile newImage, Room room);


	}
}
