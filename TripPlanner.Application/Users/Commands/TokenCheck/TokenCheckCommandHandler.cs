using AutoMapper;
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
    public class TokenCheckCommandHandler(ITokenRepository tokenRepository,IMapper mapper): IRequestHandler<TokenCheckCommand, AuthResponse>
    {
        public Task<AuthResponse> Handle(TokenCheckCommand request, CancellationToken cancellationToken)
        {
            var authRequest = new AuthResponse()
            {
                Username = request.Username,
                Token = request.Token,
                RefreshToken = request.RefreshToken
            };
            var authResponse=tokenRepository.VerifyRefreshToken(authRequest);
            return authResponse;
        }
    }
}
