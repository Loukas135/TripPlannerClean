﻿using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Trips.Commands.CreateTrip
{
	public class CreateTripCommand : IRequest<int>
	{
        [DataType(DataType.Date)]
        public DateTime From { get; set; } = default!;
        [DataType(DataType.Date)]
        public DateTime To { get; set; } = default!;
		public string Title { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int Days { get; set; }
		public float Price { get; set; }

		public int ServiceId { get; set; }
		public IFormFile? TripImage { get; set; }
	}
}
