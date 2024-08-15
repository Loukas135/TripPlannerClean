using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Commands.ChangeStatus
{
    public class ChangeReservationStatusCommandHandler(IReservationRespository reservationRespository,
        IUserContext userContext, UserManager<User> userManager): IRequestHandler<ChangeReservationStatusCommand>
    {
        public async Task Handle(ChangeReservationStatusCommand request, CancellationToken cancellationToken)
        {
            var reservation = await reservationRespository.GetById(request.reservationId);
            if (reservation == null)
            {
                throw new NotFoundException(nameof(Reservation), request.reservationId.ToString());
            }
            if (request.isAccepted == true)
            {
                reservation.Status = "Accepted";
            }
            else
            {
                reservation.Status = "Rejected";
                if (reservation.ElectronicPayment)
                {
                    var user = await userManager.FindByIdAsync(userContext.GetCurrentUser()!.Id);
                    user!.Wallet += reservation.Cost;
                }
            }
            await reservationRespository.UpdateReservation(reservation);
            
        }
    }
}
