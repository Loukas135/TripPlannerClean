using MediatR;
using Microsoft.AspNetCore.Http;
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
		public float PricePerMonth { get; set; } = default!;	
		public int Quantity { get; set; } = default!;

		public int CarCategoryId { get; set; } = default!;
		public int ServiceId { get; set; } = default!;
		public IFormFile? ImagePath { get; set; }
	}
}
