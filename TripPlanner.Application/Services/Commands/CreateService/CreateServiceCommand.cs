using MediatR;
using Microsoft.AspNetCore.Http;
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
		public int GovernorateId { get; set; }
		public int ServiceTypeId { get; set; }
		//public int Rate { get; set; }
		public string OwnerId { get; set; } = default!;
        public bool HasWiFi { get; set; } = false;
        public bool HasPool { get; set; } = false;
        public bool HasRestaurant { get; set; } = false;
		
    }
}
