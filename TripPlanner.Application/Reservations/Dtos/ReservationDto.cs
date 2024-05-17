﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Reservations.Dtos
{
	public class ReservationDto
	{
		public int Id { get; set; }
		public int Cost { get; set; }
		[DataType(DataType.Date)]
		public DateTime From { get; set; } = default!;
		[DataType(DataType.Date)]
		public DateTime To { get; set; } = default!;
		public int ServiceId { get; set; }
		public string Payment { get; set; } = default!;
		public string UserId { get; set; } = default!;
	}
}
