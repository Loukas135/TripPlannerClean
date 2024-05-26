using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.ServiceTypes.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.ServiceTypes.Queries.GetAllServiceTypes
{
    public class GetAllServiceTypesQueryHandler(IMapper mapper, IServicetypeRepository servicetypeRepository) : IRequestHandler<GetAllServiceTypesQuery, IEnumerable<ServiceTypesDto>>
    {
        async Task<IEnumerable<ServiceTypesDto>> IRequestHandler<GetAllServiceTypesQuery, IEnumerable<ServiceTypesDto>>.Handle(GetAllServiceTypesQuery request, CancellationToken cancellationToken)
        {
            var typeList = await servicetypeRepository.GetAllAsync();
            var serviceTypes = mapper.Map<IEnumerable<ServiceTypesDto>>(typeList);
            return serviceTypes;
        }
    }
}
