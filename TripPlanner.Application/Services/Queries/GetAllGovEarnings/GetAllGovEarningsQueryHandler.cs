using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;

namespace TripPlanner.Application.Services.Queries.GetAllGovEarnings
{
    internal class GetAllGovEarningsQueryHandler : IRequestHandler<GetAllGovEarningsQuery, GovEarningsDto>
    {
        public Task<GovEarningsDto> Handle(GetAllGovEarningsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
