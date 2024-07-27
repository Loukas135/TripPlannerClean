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
		public string Title { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int NumberOfPeople { get; set; } = default!;
		public int Quantity { get; set; } = default!;
		public float PricePerNight { get; set; } = default!;

		public int RoomCategoryId { get; set; } = default!;
		public int ServiceId { get; set; } = default!;
		public IFormFile? RoomImage { get; set; }
	}
}
