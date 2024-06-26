﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;

namespace TripPlanner.Application.Reservations.Queries.GetCarReservations
{
	public class GetCarReservationsQuery(int serId) : IRequest<IEnumerable<CarReservationDto?>>
	{
		public int ServiceId { get; } = serId;
	}
}
