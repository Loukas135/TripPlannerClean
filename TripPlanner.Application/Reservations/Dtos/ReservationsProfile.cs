using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;

namespace TripPlanner.Application.Reservations.Dtos
{
	public class ReservationsProfile : Profile
	{
        public ReservationsProfile()
        {
            CreateMap<Reservation, ReservationDto>();
        }
    }
}
