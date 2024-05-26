using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Governorates.Dtos;

namespace TripPlanner.Application.Governorates.Queries
{
    public class GetGovernoreatesQuery:IRequest<IEnumerable<GovernoratesDto>>
    {
    }
}
