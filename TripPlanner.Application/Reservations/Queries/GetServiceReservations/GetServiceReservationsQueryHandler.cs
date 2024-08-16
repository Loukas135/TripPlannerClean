using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Queries.GetServiceReservations
{
	public class GetServiceReservationsQueryHandler(IServiceRepository serviceRepository,
        IAccountRepository accountRepository,
		IMapper mapper) : IRequestHandler<GetServiceReservationsQuery, IEnumerable<ReservationDto?>>
	{
		public async Task<IEnumerable<ReservationDto?>> Handle(GetServiceReservationsQuery request, CancellationToken cancellationToken)
		{
			var service = await serviceRepository.GetServiceWithReservations(request.ServiceId);

			var reservations = service!.Reservations?.ToList();

            IList<ReservationDto> results = [];
            foreach (var reservation in reservations)
            {
                var user = await accountRepository.GetUserAsync(reservation.UserId);
                var result = new ReservationDto
                {
                    Id = reservation.Id,
                    ServiceId = reservation.ServiceId,
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
