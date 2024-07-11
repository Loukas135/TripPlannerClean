using MediatR;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Reservations.Commands.Room
{
	public class ReserveRoomCommand : IRequest<int>
	{
		public int ServiceId { get; set; }
		public int RoomId { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime From { get; set; } = default!;
        [DataType(DataType.Date)]
        public DateTime To { get; set; } = default!;
		public string Payment { get; set; } = default!;
	}
}
