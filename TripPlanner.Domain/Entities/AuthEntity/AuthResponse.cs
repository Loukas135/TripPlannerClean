using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Entities.AuthEntity
{
    public class AuthResponse
    {
        public string? Token { get; set; }
        public string? Username { get; set; }
        public int? Expires { get; set; }
        public string? RefreshToken { get; set; }
        public string? Role { get; set; }
    
    }
}
