using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;

namespace TripPlanner.Application.Services.Queries.GetAllGovEarnings
{
    public class GetAllGovEarningsQuery(int month,int year):IRequest<GovEarningsDto>
    {
        public int Month { get; set; }= month;
        public int Year { get; set; } = year;
    }
}
