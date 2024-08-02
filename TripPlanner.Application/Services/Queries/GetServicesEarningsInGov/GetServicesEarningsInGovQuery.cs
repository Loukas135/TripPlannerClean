using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Application.Services.Queries.GetServicesEarningsInGov
{
    public class GetServicesEarningsInGovQuery(int govId,int year,int month):IRequest<IEnumerable<ServiceEarningsDto>>
    {
        public int GovId { get; set; } = govId;
        public int Year { get; set; } = year;
        public int Month { get; set; } = month;
    }
}
