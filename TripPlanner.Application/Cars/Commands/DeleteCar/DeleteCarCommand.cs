using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Cars.Commands.DeleteCar
{
	public class DeleteCarCommand(int serId, int carId) : IRequest
	{
		public int ServiceId { get; } = serId;
		public int CarId { get; } = carId;
	}
}
