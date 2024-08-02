using AutoMapper;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Queries.GetServiceReservations.GetReservationsInTimeForService
{
    internal class GetReservationsInTimeForServiceQueryHandler(IReservationRespository reservationRespository
        ,IMapper mapper
        ,IServiceRepository serviceRepository) : IRequestHandler<GetReservationsInTimeForServiceQuery, IEnumerable<ReservationDto>>
    {
        public async Task<IEnumerable<ReservationDto>> Handle(GetReservationsInTimeForServiceQuery request, CancellationToken cancellationToken)
        {
            var service = await serviceRepository.GetServiceWithReservations(request.ServiceId);
            if (service == null)
            {
                return null;
            }
            var reservations = service.Reservations.Where(r => r.From.Year == request.Year 
            && r.From.Month == request.Month).ToList();
            if (reservations == null)
            {
                return null;
            }
           var reservationDto= mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return reservationDto;
        }
    }
}
