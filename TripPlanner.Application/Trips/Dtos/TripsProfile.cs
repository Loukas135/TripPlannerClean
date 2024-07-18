using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Trips.Commands.CreateTrip;
using TripPlanner.Application.Trips.Commands.UpdateTrip;
using TripPlanner.Domain.Entities.Service_Entities.Tourism_Office;

namespace TripPlanner.Application.Trips.Dtos
{
	public class TripsProfile : Profile
	{
        public TripsProfile()
        {
			CreateMap<UpdateTripCommand, Trip>();

			CreateMap<Trip, TripDto>()
				.ForMember(d => d.Reservations, opt => opt
					.MapFrom(src => src.Reservations));

			CreateMap<CreateTripCommand, Trip>();
		}
    }
}
