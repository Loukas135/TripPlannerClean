using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TripPlanner.Application.Users.Commands.RegisterUser
{
	public class RegisterUserCommand : IRequest<IEnumerable<IdentityError>>
	{
		public string UserName { get; set; } = default!;
		[EmailAddress]
		public string Email { get; set; } = default!;
		[PasswordPropertyText]
		public string Password { get; set; } = default!;
	}
}
