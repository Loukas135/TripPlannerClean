using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;
using TripPlanner.Domain.Exceptions;

namespace TripPlanner.Application.Reservations.Queries.GetRoomReservations
{
	public class GetRoomReservationQueryHandler(IServiceRepository serviceRepository,
        IAccountRepository accountRepository,
		IMapper mapper) : IRequestHandler<GetRoomReservationQuery, IEnumerable<RoomReservationDto>>
	{
		public async Task<IEnumerable<RoomReservationDto>> Handle(GetRoomReservationQuery request, CancellationToken cancellationToken)
		{
			var service = await serviceRepository.GetServiceWithReservations(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}
			var reservations = service.Reservations?.ToList();

            IList<RoomReservationDto> results = [];
            foreach (var reservation in reservations)
            {
                var user = await accountRepository.GetUserAsync(reservation.UserId);
                var result = new RoomReservationDto
                {
                    Id = reservation.Id,
                    ServiceId = reservation.ServiceId,
                    RoomId = (int)reservation.RoomId!,
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
