using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Queries.GetTripReservations
{
	public class GetTripReservationQueryHandler(IServiceRepository serviceRepository,
		IAccountRepository accountRepository,
		IMapper mapper) : IRequestHandler<GetTripReservationQuery, IEnumerable<TripReservationDto>>
	{
		public async Task<IEnumerable<TripReservationDto>> Handle(GetTripReservationQuery request, CancellationToken cancellationToken)
		{
			var service = await serviceRepository.GetServiceWithReservations(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}
			var reservations = service.Reservations?.ToList();

            IList<TripReservationDto> results = [];
            foreach (var reservation in reservations)
            {
                var user = await accountRepository.GetUserAsync(reservation.UserId);
                var result = new TripReservationDto
                {
                    Id = reservation.Id,
                    ServiceId = reservation.ServiceId,
                    TripId = (int)reservation.TripId!,
                    Cost = reservation.Cost,
                    Status = reservation.Status,
                    ElectronicPayment = reservation.ElectronicPayment,
                    UserName = user.UserName!,
                    From = reservation.From,
                    To = reservation.To,
                };
                results.Add(result);
            }
            return results;
		}
	}
}
