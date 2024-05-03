using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Cars.Dtos;

namespace TripPlanner.Application.Cars.Queries.GetCarById
{
	public class GetCarByIdQuery(int serId, int carId) : IRequest<CarDto>
	{
		public int ServiceId { get; } = serId;
		public int CarId { get; } = carId;
	}
}
