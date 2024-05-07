using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Users.Commands.LoginUser
{
	public class LoginUserCommand : IRequest<string>
	{
		public string Email { get; set; } = default!;
		public string Password { get; set; } = default!;
	}
}
