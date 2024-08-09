using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TripPlanner.Application.Users.Commands.RegisterUser
{
	public class RegisterUserCommand : IRequest<IEnumerable<IdentityError>>
	{
		public string UserName { get; set; } = default!;
		[EmailAddress]
		public string Email { get; set; } = default!;
		[PasswordPropertyText]
		public string Password { get; set; } = default!;
		[AllowNull]
		public string? BaseUrl { get; set; }
		public IFormFile? UserProfile { get; set; }
	}
}
