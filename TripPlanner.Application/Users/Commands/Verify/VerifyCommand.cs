using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Users.Commands.Verify
{
	public class VerifyCommand(string token) : IRequest<bool>
	{
		public string VerificationToken { get; } = token;
	}
}
