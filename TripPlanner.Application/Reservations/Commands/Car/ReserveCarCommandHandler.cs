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
using TripPlanner.Domain.Entities.Service_Entities.Tourism_Office;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Commands.Car
{
	public class ReserveCarCommandHandler(ILogger<ReserveCarCommandHandler> logger,
		ICarRepository carRepository,
		IMapper mapper,
		IUserContext userContext,
		UserManager<User> userManager,
		IReservationRespository reservationRespository) : IRequestHandler<ReserveCarCommand, int>
	{
		public async Task<int> Handle(ReserveCarCommand request, CancellationToken cancellationToken)
		{
			var userId = userContext.GetCurrentUser().Id;
			var user = await userManager.FindByIdAsync(userId);

			var car = await carRepository.GetById(request.CarId);
			if (car == null)
			{
				throw new NotFoundException(nameof(Car), request.CarId.ToString());
			}

			var reservation = mapper.Map<Reservation>(request);
			reservation.CarId = request.CarId;
			reservation.UserId = userId;
			reservation.ServiceId = request.ServiceId;
			reservation.From = request.From;
			reservation.To = request.To;
			reservation.Cost = request.Months * (int) car.PricePerMonth;
			reservation.Payment = request.Payment;

			if (request.Payment == "Electronic")
			{
				if (user.Wallet > (int) reservation.Cost)
				{
					user!.Wallet -= (int) reservation.Cost;
					await userManager.UpdateAsync(user);
				}else
				{
					throw new NoBalanceException("No Enough Balance for this operation");
				}
			}

			return await reservationRespository.Add(reservation);
		}
	}
}
