using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;

namespace TripPlanner.Application.Services.Queries.GetSericeWith1Id
{
    public class GetServiceWith1IdQuery(int serviceId) : IRequest<ServiceDto>
    {
       public int ServiceId { get; set; } = serviceId;
    }
}
