using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.ServiceTypes.Dtos;

namespace TripPlanner.Application.ServiceTypes.Queries.GetAllServiceTypes
{
    public class GetAllServiceTypesQuery : IRequest<IEnumerable<ServiceTypesDto>>
    {
        public GetAllServiceTypesQuery()
        {

        }
    }
}
