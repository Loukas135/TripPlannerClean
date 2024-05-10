using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Rating.Commands.AddRating
{
    internal class AddRatingCommandHandler(IMapper mapper,
        IUserContext userContext,
        IServiceRepository serviceRepository,
        IRatingRepository ratingRepository) : IRequestHandler<AddRatingCommand, int>
    {
        public async Task<int> Handle(AddRatingCommand request, CancellationToken cancellationToken)
        {
            var userId = userContext.GetCurrentUser().Id;
            var rating = mapper.Map<Ratings>(request);
            var rating_id = await ratingRepository.AddRating(rating);
            await serviceRepository.CalculateOverallRating((int)request.ServiceId);
            return rating_id;
        }
    }
}
