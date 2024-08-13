using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Services.Queries.GetSericeWith1Id
{
    internal class GetServiceWith1IdQueryHandler(IMapper mapper, IServiceRepository serviceRepository) : IRequestHandler<GetServiceWith1IdQuery,ServiceDto>
    {
      
        public async Task<ServiceDto> Handle(GetServiceWith1IdQuery request, CancellationToken cancellationToken)
        {
            var service = await serviceRepository.GetByIdWithImages(request.ServiceId);
            if (service == null)
            {
                return null;
            }
            var response=mapper.Map<ServiceDto>(service);
            return response;
        }
    }
}
