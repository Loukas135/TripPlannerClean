using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Users.Queries
{
    internal class GetCurrentUserQueryHandler(IUserContext userContext) : IRequestHandler<GetCurrentUserQuery, CurrentUser>
    {
        
        public async Task<CurrentUser> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var current_User = userContext.GetCurrentUser();
            return current_User;
        }
    
}
}
