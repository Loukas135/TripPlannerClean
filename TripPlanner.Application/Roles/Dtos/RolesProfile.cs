using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Roles.Dtos
{
    public class RolesProfile:Profile
    {
        public RolesProfile()
        {
            CreateMap<IdentityRole, RolesDto>().ReverseMap();
        }
    }
}
