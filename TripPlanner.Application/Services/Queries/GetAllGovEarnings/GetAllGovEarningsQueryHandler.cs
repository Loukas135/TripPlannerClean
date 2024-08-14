using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Domain;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Services.Queries.GetAllGovEarnings
{
    internal class GetAllGovEarningsQueryHandler(IReservationRespository reservationRespository)
        : IRequestHandler<GetAllGovEarningsQuery, IEnumerable<AllGovsEarnings>>
    {
        public Task<IEnumerable<AllGovsEarnings>> Handle(GetAllGovEarningsQuery request, CancellationToken cancellationToken)
        {
            var reservations = reservationRespository.GetAllGovsEarnings(request.Month, request.Year);
            return reservations;
		}
    }
}
