using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;

namespace TripPlanner.Application.Services.Queries.GetServiceByType
{
    public class GetServiceByTypeQuery:IRequest<IEnumerable<ServiceDto>>
    {
       public int governorateId {  get; set; }
        public int serviceTypeId { get; set; }
        public GetServiceByTypeQuery(int governorateId, int serviceTypeId)
        {
            this.governorateId = governorateId;
            this.serviceTypeId = serviceTypeId;
        }
    }
}
