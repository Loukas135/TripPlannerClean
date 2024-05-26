using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Roles.Dtos;

namespace TripPlanner.Application.Roles.Queries
{
    public class GetRolesQuery:IRequest<IEnumerable<RolesDto>>
    {
    }
}
