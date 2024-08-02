using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Services.Queries.GetServiceEarnings
{
    internal class GetServiceEarningsQueryHandler(IServiceRepository serviceRepository)
        : IRequestHandler<GetServiceEarningsQuery, ServiceEarningsDto>
    {
        public async Task<ServiceEarningsDto> Handle(GetServiceEarningsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await serviceRepository.PaidReservations(request.ServiceId
                , request.Year
                , request.Month
                );
            var sum = 0;
            foreach (var reservation in reservations)
            {
                sum += reservation.Cost;
            }
            return new ServiceEarningsDto {
                ServiceId = request.ServiceId,
                Year = request.Year,
                Month = request.Month,
                Earnings = sum
            };
        }
    }
}
