using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;

namespace TripPlanner.Application.Governorates.Dtos
{
    public class GovernoratesProfile:Profile
    {
        public GovernoratesProfile()
        {
            CreateMap<Governorate, GovernoratesDto>().ReverseMap();
        }
    }
}
