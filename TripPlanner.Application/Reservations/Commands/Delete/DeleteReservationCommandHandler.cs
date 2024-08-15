using MediatR;
using TripPlanner.Domain.Repositories;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using TripPlanner.Application.Users;

namespace TripPlanner.Application.Reservations.Commands.Delete
{
	public class DeleteReservationCommandHandler(IReservationRespository reservationRespository,
		IUserContext userContext, UserManager<User> userManager) : IRequestHandler<DeleteReservationCommand>
	{
		public async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
		{
			var reservation = await reservationRespository.GetById(request.ReservationId);
			if (reservation == null)
			{
				throw new NotFoundException(nameof(Reservation), "That reservation doesnt exist");
			}

			if (reservation.ElectronicPayment)
			{
				var userId = userContext.GetCurrentUser()!.Id;
				var user = await userManager.FindByIdAsync(userId);
				user!.Wallet += reservation.Cost;
				await userManager.UpdateAsync(user);
			}

			await reservationRespository.DeleteReservation(reservation);
			
			
		}
	}
}
