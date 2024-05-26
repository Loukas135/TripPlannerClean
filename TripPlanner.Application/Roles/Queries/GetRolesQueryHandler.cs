using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Roles.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Roles.Queries
{
    public class GetRolesQueryHandler(IRolesRepository rolesRepository,IMapper mapper) : IRequestHandler<GetRolesQuery, IEnumerable<RolesDto>>
    {
        public async Task<IEnumerable<RolesDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await rolesRepository.GetAllAsync();
            var allRoles = mapper.Map <IEnumerable<RolesDto>>(roles);
            return allRoles;
        }
    }
}
