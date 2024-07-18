using AutoMapper;
using TripPlanner.Application.Cars.Commands.CreateCar;
using TripPlanner.Application.Cars.Commands.UpdateCar;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;

namespace TripPlanner.Application.Cars.Dtos
{
	public class CarsProfile : Profile
	{
        public CarsProfile()
        {
			CreateMap<UpdateCarCommand, Car>();

			CreateMap<Car, CarDto>()
				.ForMember(d => d.Reservations, opt => opt
					.MapFrom(src => src.Reservations));

			CreateMap<CreateCarCommand, Car>();
		}
	}
}
