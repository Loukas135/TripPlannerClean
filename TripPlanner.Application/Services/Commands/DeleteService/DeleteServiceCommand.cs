using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Services.Commands.DeleteService
{
	public class DeleteServiceCommand(int id) : IRequest<bool>
	{
		public int Id { get; } = id;
	}
}
