using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Trips.Commands.UpdateTrip
{
	public class UpdateTripCommand : IRequest
	{
		public int TripId { get; set; } = default!;
		[DataType(DataType.Date)]
		public DateTime? From { get; set; }
		[DataType(DataType.Date)]
		public DateTime? To { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int? Days { get; set; }
		public float? Price { get; set; }
		public IFormFile? TripImage { get; set; }
	}
}
