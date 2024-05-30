using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Users.Commands.Verify
{
	public class VerifyCommand : IRequest<bool>
	{
		public string Email { get; set; } = default!;
		public string VerificationToken { get; set; } = default!;
	}
}
