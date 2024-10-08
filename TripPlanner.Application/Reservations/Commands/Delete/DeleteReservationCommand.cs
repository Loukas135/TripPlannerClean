﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Reservations.Commands.Delete
{
	public class DeleteReservationCommand(int reservationId) : IRequest
	{
		public int ReservationId { get; set; }
	}
}
