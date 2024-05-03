using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;

namespace TripPlanner.Application.Services.Queries.GetAllServices
{
	public class GetAllServicesQuery(int govId/*, int stId*/) : IRequest<IEnumerable<ServiceDto>?>
	{
		public int GovernorateId { get; set; } = govId;
		//public int ServiceTypeId { get; set; } = stId;
	}
}
