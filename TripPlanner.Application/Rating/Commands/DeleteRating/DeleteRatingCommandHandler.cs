using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Rating.Commands.DeleteRating
{
    public class DeleteRatingCommandHandler (IRatingRepository ratingRepository) : IRequestHandler<DeleteRatingCommand>
    {
        public Task Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
