using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Users.Dtos
{
    public class FullCurrentUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImagePath { get; set; }
        public int Wallet { get; set; }
        public string Role { get; set; } = "User";
    }
}
