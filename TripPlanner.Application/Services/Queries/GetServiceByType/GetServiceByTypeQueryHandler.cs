using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Services.Queries.GetServiceByType
{
    public class GetServiceByTypeQueryHandler(IMapper mapper,IServiceRepository serviceRepository) : IRequestHandler<GetServiceByTypeQuery, IEnumerable<ServiceDto>>
    {
        public async Task<IEnumerable<ServiceDto>> Handle(GetServiceByTypeQuery request, CancellationToken cancellationToken)
        {
            var services = await serviceRepository.GetServicesOfType(request.governorateId, request.serviceTypeId);
            var serviceDto=mapper.Map<IEnumerable<ServiceDto>>(services);
            return serviceDto;
        }
    }
}
