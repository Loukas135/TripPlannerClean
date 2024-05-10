using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Rating.Commands.AddRating;
using TripPlanner.Application.Rating.Commands.DeleteRating;
using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Application.Rating.Dtos
{
    public class RatingsProfile : Profile
    {
        public RatingsProfile()
        {
            CreateMap<AddRatingCommand, Ratings>().ReverseMap();
            CreateMap<DeleteRatingCommand, Ratings>().ReverseMap();
        }
    }
}
