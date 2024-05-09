using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Users.Commands.Register
{
    public class RegisterCommand :IRequest<IEnumerable <IdentityError>>
    {
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        [EmailAddress]
        public string Email { get; set; } = default!;

        [StringLength(16, ErrorMessage = "Your Password is limited from 6 to 16 characters.", MinimumLength = 6)]
        public string Password { get; set; } = default!;
    }
}
