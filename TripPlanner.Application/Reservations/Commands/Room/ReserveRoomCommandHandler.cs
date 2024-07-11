using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Commands.Room
{
	public class ReserveRoomCommandHandler(ILogger<ReserveRoomCommandHandler> logger,
		IMapper mapper,
		IReservationRespository reservationRespository,
		IUserContext userContext,
		UserManager<User> userManager,
		IRoomRepository roomRepository) : IRequestHandler<ReserveRoomCommand, int>
	{
		public async Task<int> Handle(ReserveRoomCommand request, CancellationToken cancellationToken)
		{
			var userId = userContext.GetCurrentUser().Id;
			var user = await userManager.FindByIdAsync(userId);

			var room = await roomRepository.GetById(request.RoomId);
			if (room == null)
			{
				throw new NotFoundException(nameof(TripPlanner.Domain.Entities.Service_Entities.Hotel.Room),
					request.RoomId.ToString());
			}

			if(room.Quantity == 0)
			{
				throw new NoResourceAvailable(nameof(Domain.Entities.Service_Entities.Hotel.Room));
			}

			var reservation = mapper.Map<Reservation>(request);
			reservation.UserId = userId;
			reservation.RoomId = room.Id;
			reservation.ServiceId = request.ServiceId;
			int monthnights = (request.To.Month - request.From.Month)*30;
			int nights = request.To.Day - request.From.Day+monthnights;
			reservation.Cost = nights * (int)room.PricePerNight;
			reservation.From = request.From;
			reservation.To = request.To;
            reservation.ElectronicPayment = (request.Payment == "Electronic");

			if (request.Payment == "Electronic")
			{
				if (user!.Wallet >= (int)reservation.Cost)
				{
					user!.Wallet -= (int)reservation.Cost;
					await userManager.UpdateAsync(user);
				}else
				{
					throw new NoBalanceException("No Enough Balance for this operation");
				}
			}

			room.Quantity--;
			await roomRepository.SaveChanges();
			return await reservationRespository.Add(reservation);
		}
	}
}
