using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Commands.Car;
using TripPlanner.Application.Reservations.Commands.Room;
using TripPlanner.Application.Reservations.Commands.Trips;
using TripPlanner.Domain.Entities;

namespace TripPlanner.Application.Reservations.Dtos
{
	public class ReservationsProfile : Profile
	{
        public ReservationsProfile()
        {
            CreateMap<Reservation, ReservationDto>();
            CreateMap<Reservation, CarReservationDto>();
            CreateMap<Reservation, RoomReservationDto>();
            CreateMap<Reservation, TripReservationDto>();

            CreateMap<ReserveTripCommand, Reservation>();
            CreateMap<ReserveCarCommand, Reservation>();
            CreateMap<ReserveRoomCommand, Reservation>();
        }
    }
}
