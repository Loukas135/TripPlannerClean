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

namespace TripPlanner.Application.Services.Queries.GetUserServices
{
    public class GetUserServicesQueryHandler(IServiceRepository serviceRepository,
        IMapper mapper,IUserContext userContext) : IRequestHandler<GetUserServicesQuery, ServiceDto>
    {
        public async Task<ServiceDto> Handle(GetUserServicesQuery request, CancellationToken cancellationToken)
        {
            var userId = userContext.GetCurrentUser().Id;
            var service = await serviceRepository.GetByUserId(userId);
            var serviceDto = mapper.Map<ServiceDto>(service);
            return serviceDto;
        }
    }
}
