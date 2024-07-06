using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Commands.ChangeStatus
{
    public class ChangeReservationStatusCommandHandler(IReservationRespository reservationRespository): IRequestHandler<ChangeReservationStatusCommand>
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
                reservation.Status = "accepted";
            }
            else
            {
                reservation.Status = "rejected";
            }
            await reservationRespository.UpdateReservation(reservation);
            return;
            
        }
    }
}
