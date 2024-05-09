using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.AuthEntity;

namespace TripPlanner.Application.Users.Commands.TokenCheck
{
    public class RefreshTokenRequestCommand : IRequest<AuthResponse>
    {
        public string? Token { get; set; }
        public string? Username { get; set; }
        public string? RefreshToken { get; set; }
    }
}
