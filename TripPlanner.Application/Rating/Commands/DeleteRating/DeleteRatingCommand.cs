using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Rating.Commands.DeleteRating
{
    public class DeleteRatingCommand : IRequest
    {
        int serviceId { get; set; }
        public DeleteRatingCommand(int serviceId)
        {
            this.serviceId = serviceId;
        }
    }
}
