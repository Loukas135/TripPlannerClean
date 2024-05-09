using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.AuthEntity;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Commands.TokenCheck
{
    internal class RefreshTokenRequestCommandHandler(ITokenRepository tokenRepository,IUserContext userContext) : IRequestHandler<RefreshTokenRequestCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(RefreshTokenRequestCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser().Id.ToString();
            var req = new RefreshTokenRequest
            {
                user_id = user,
                RefreshToken = request.RefreshToken
            };
            var response = await tokenRepository.VerifyRefreshToken(req);
            return response;
        }
    }
}
