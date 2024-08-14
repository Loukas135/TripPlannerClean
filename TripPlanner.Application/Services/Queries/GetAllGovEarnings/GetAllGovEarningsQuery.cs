using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Domain;

namespace TripPlanner.Application.Services.Queries.GetAllGovEarnings
{
    public class GetAllGovEarningsQuery(int month,int year):IRequest<IEnumerable<AllGovsEarnings>>
    {
        public int Month { get; set; }= month;
        public int Year { get; set; } = year;
    }
}
