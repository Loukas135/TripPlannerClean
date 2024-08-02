using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;

namespace TripPlanner.Application.Reservations.Queries.GetServiceReservations.GetReservationsInTimeForService
{
    public class GetReservationsInTimeForServiceQuery(int serviceId, int year, int month) : IRequest<IEnumerable<ReservationDto>>
    {
        public int ServiceId { get; set; } = serviceId;
        public int Year { get; set; } = year;
        public int Month { get; set; } = month;
    }
}
