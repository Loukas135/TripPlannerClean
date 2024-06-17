using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Entities.AuthEntity
{
    public class UserSeedingRequest
    {
        public string UserName { get; set; } = default!;
        [EmailAddress]
        public string Email { get; set; } = default!;
        [PasswordPropertyText]
        public string Password { get; set; } = default!;
        public string? Role { get; set; }
    }
}
