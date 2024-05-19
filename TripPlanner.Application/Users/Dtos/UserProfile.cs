using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users.Commands.Register;
using TripPlanner.Application.Users.Commands.RegisterUser;
using TripPlanner.Domain.Entities;

namespace TripPlanner.Application.Users.Dtos
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterServiceOwnerCommand>().ReverseMap();
            CreateMap<RegisterUserCommand, User>();
        }
    }
}
