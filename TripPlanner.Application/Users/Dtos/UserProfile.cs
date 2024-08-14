using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users.Commands.Register;
using TripPlanner.Application.Users.Commands.RegisterAdmin;
using TripPlanner.Application.Users.Commands.RegisterUser;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.AuthEntity;

namespace TripPlanner.Application.Users.Dtos
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterServiceOwnerCommand>().ReverseMap();
            CreateMap<RegisterUserCommand, User>();
            CreateMap<RegisterAdminCommand, User>().ReverseMap();
            CreateMap<UserSeedingRequest, User>().ReverseMap();
            CreateMap<User, FullCurrentUserDto>().ReverseMap();
        }
    }
}
