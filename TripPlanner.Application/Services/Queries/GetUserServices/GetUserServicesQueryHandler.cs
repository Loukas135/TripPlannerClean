using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Application.Users;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Services.Queries.GetUserServices
{
    internal class GetUserServicesQueryHandler(IServiceRepository serviceRepository,IMapper mapper, IUserContext userContext, UserManager<User> userManager) : IRequestHandler<GetUserServicesQuery, IEnumerable<ServiceDto>>
    {
        public async Task<IEnumerable<ServiceDto>> Handle(GetUserServicesQuery request, CancellationToken cancellationToken)
        {
            var user_Id = userContext.GetCurrentUser().Id.ToString();
            var services = await serviceRepository.GetServicesOfUser(user_Id);
            var servicesDto = mapper.Map<IEnumerable<ServiceDto>>(services);
            return servicesDto;
        }
    }
}
