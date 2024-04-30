using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;

namespace TripPlanner.Application.Services.Queries.GetServiceById
{
	public class GetServiceByIdQuery(int id) : IRequest<ServiceDto?>
	{
		public int Id { get; } = id;
	}
}
