using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Application.Users;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Services.Queries.GetServiceFromUserReservation
{
    public class GetServiceFromUserReservationQueryHandler(IMapper mapper,IUserContext userContext,
        IServiceRepository serviceRepository) :
        IRequestHandler<GetServiceFromUserReservationQuery, IEnumerable<ServiceDto>>
    {
        public async Task<IEnumerable<ServiceDto>> Handle(GetServiceFromUserReservationQuery request, CancellationToken cancellationToken)
        {
            var user_id = userContext.GetCurrentUser().Id.ToString();
            request.userId = user_id;
            var services = await serviceRepository.GetServiceFromUserReservation(request.userId);
            var result = mapper.Map<IEnumerable<ServiceDto>>(services);
            return result;
        }
    }
}
