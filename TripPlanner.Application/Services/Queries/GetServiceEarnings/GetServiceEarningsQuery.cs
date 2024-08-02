using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;

namespace TripPlanner.Application.Services.Queries.GetServiceEarnings
{
    public class GetServiceEarningsQuery(int serviceId, int year, int month) : IRequest<ServiceEarningsDto>
    {
        public int ServiceId { get; set; } = serviceId;
        public int Year { get; set; } = year;
        public int Month { get; set; } = month;
    }
}
