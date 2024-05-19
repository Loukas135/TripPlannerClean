using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.Service_Entities.Tourism_Office;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Commands.Trips
{
	public class ReserveTripCommandHandler(IMapper mapper,
		IReservationRespository reservationRespository,
		ITripRepository tripRepository,
		IUserContext userContext,
		UserManager<User> userManager) : IRequestHandler<ReserveTripCommand, int>
	{
		public async Task<int> Handle(ReserveTripCommand request, CancellationToken cancellationToken)
		{
			var trip = await tripRepository.GetById(request.TripId);
			if(trip == null )
			{
				throw new NotFoundException(nameof(Trip), request.TripId.ToString());
			}

			var user = userContext.GetCurrentUser();

			var reservation = mapper.Map<Reservation>(request);
			reservation.ServiceId = request.ServiceId;
			reservation.Payment = request.Payment;
			reservation.Cost = (int) trip.Price;
			reservation.UserId = user.Id;
			reservation.From = trip.From;
			reservation.To = trip.To;

			if(request.Payment == "Electronic")
			{
				var userWallet = await userManager.FindByIdAsync(user.Id);
				if(userWallet!.Wallet > trip.Price)
				{
					userWallet!.Wallet -= (int)trip.Price;
					await userManager.UpdateAsync(userWallet);
				}else
				{
					throw new NoBalanceException("No Enough Balance for this operation");
				}
			}

			return await reservationRespository.Add(reservation);
		}
	}
}
