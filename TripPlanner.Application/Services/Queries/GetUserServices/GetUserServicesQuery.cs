using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;

namespace TripPlanner.Application.Services.Queries.GetUserServices
{
    public class GetUserServicesQuery:IRequest<ServiceDto>
    {
       public string userId { get; set; }
    }
}
