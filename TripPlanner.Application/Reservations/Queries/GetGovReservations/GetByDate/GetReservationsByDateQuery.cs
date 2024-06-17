using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;

namespace TripPlanner.Application.Reservations.Queries.GetGovReservations.GetByDate
{
	public class GetReservationsByDateQuery(int year, int month, int govId) : IRequest<IEnumerable<ReservationDto>>
	{
		public int GovId { get; } = govId;
		public int Year { get; } = year;
		public int Month { get; } = month;
		/*
		public string Year { get; } = $"0{year}";
		public string Year { get; } = $"0{month}";
		*/
	}
}
