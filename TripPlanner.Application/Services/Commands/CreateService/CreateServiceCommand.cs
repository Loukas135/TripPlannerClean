using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Services.Commands.CreateService
{
	public class CreateServiceCommand : IRequest<int>
	{
		public string Name { get; set; } = default!;
		public string Address { get; set; } = default!;
		public string Description { get; set; } = default!;
		public string ContactNumber { get; set; } = default!;
		public string? ContactEmail { get; set; }
	}
}
