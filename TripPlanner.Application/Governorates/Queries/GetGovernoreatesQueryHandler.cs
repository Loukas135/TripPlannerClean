using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Governorates.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Governorates.Queries
{
    public class GetGovernoreatesQueryHandler(IMapper mapper,IGovernoratesRepository governoratesRepository) : IRequestHandler<GetGovernoreatesQuery, IEnumerable<GovernoratesDto>>
    {
        public async Task<IEnumerable<GovernoratesDto>> Handle(GetGovernoreatesQuery request, CancellationToken cancellationToken)
        {
            var gov = await governoratesRepository.GetAllAsync();
            var governoratesDto = mapper.Map<IEnumerable<GovernoratesDto>>(gov);
            return governoratesDto;
        }
    }
}
