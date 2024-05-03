using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Cars.Dtos;

namespace TripPlanner.Application.Cars.Queries.GetAllCars
{
	public class GetAllCarsQuery(int serId) : IRequest<IEnumerable<CarDto>>
	{
		public int ServiceId { get; } = serId;
	}
}
