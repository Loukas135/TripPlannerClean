using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Services.Queries.GetServicesEarningsInGov
{
    internal class GetServicesEarningsInGovQueryHandler(IGovernoratesRepository governoratesRepository,
        IServiceRepository serviceRepository) : IRequestHandler<GetServicesEarningsInGovQuery, IEnumerable<ServiceEarningsDto>>
    {
        public async Task<IEnumerable<ServiceEarningsDto>> Handle(GetServicesEarningsInGovQuery request, CancellationToken cancellationToken)
        {
            var governorate= await governoratesRepository.GetById(request.GovId);
            if (governorate == null)
            {
                return null;
            }
            var services = governorate.Services.ToList();
            if (services == null)
            {
                return null;
            }
            IList<ServiceEarningsDto> allEarningsInGov = [];
            foreach (var service in services)
            {
                allEarningsInGov.Add(new ServiceEarningsDto
                {
                    ServiceId = service.Id,
                    Month = request.Month,
                    Year = request.Year,
                    Earnings = await serviceRepository.PaidReservationsSum(service.Id, request.Year, request.Month)
                }
                );
            }
            return allEarningsInGov;
        }
    }
}
