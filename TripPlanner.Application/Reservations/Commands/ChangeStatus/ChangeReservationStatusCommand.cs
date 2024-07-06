using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Reservations.Commands.ChangeStatus
{
    public class ChangeReservationStatusCommand :IRequest
    {
        public int reservationId { get; set; }
        public bool isAccepted { get; set; }
        public ChangeReservationStatusCommand(int reservationId, bool isAccepted)
        {
            this.reservationId = reservationId;
            this.isAccepted = isAccepted;
        }
    }
}
