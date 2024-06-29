using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Cars.Commands.CreateCar
{
	public class CreateCarCommand() : IRequest<int>
	{
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public float PricePerMonth { get; set; }
		public int Quantity { get; set; }

		public int CarCategoryId { get; set; }
		public int ServiceId { get; set; }
		public string imagePath { get; set; }
	}
}
