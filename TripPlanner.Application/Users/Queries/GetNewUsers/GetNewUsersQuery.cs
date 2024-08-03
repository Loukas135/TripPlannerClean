using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users.Dtos;

namespace TripPlanner.Application.Users.Queries.GetNewUsers
{
    public class GetNewUsersQuery : IRequest<IEnumerable<NumberOfUsersDto>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public GetNewUsersQuery(int month,int year)
        {
            this.Month = month;
            this.Year = year;
        }
    }
}
