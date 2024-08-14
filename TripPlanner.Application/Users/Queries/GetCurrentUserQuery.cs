using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users.Dtos;

namespace TripPlanner.Application.Users.Queries
{
    public class GetCurrentUserQuery:IRequest<FullCurrentUserDto>
    {

    }
}
