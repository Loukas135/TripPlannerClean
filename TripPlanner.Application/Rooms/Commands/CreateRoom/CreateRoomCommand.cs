using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Rooms.Commands.CreateRoom
{
	public class CreateRoomCommand() : IRequest<int>
	{
		public string Description { get; set; } = default!;
		public int NumberOfPeople { get; set; }
		public int Quantity { get; set; }
		public float PricePerNight { get; set; }

		public int RoomCategoryId { get; set; }
		public int ServiceId { get; set; }
		public IFormFile? RoomImage { get; set; }
	}
}
