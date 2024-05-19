using MediatR;
using Microsoft.AspNetCore.Identity;

namespace TripPlanner.Application.Users.Commands.RegisterUser
{
	public class RegisterUserCommand : IRequest<IEnumerable<IdentityError>>
	{
		public string Email { get; set; } = default!;
		public string Password { get; set; } = default!;
	}
}
