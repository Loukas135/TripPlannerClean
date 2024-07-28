using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Cars.Commands.UpdateCar
{
	public class UpdateCarCommand : IRequest
	{
		
		public int? CarId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public float? PricePerMonth { get; set; }
		public int? Quantity { get; set; }

		public int? CarCategoryId { get; set; } = default!;
		public IFormFile? ImagePath { get; set; }
	}
}
