using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Queries.GetCarReservations
{
	public class GetCarReservationsQueryHandler(ICarRepository carRepository,
		IMapper mapper) : IRequestHandler<GetCarReservationsQuery, IEnumerable<ReservationDto?>>
	{
		public async Task<IEnumerable<ReservationDto?>> Handle(GetCarReservationsQuery request, CancellationToken cancellationToken)
		{
			var car = await carRepository.GetById(request.CarId);
			if(car == null)
			{
				throw new NotFoundException(nameof(Car), request.CarId.ToString());
			}

			var reservations = car.Reservations?.ToList();

			var results = mapper.Map<IEnumerable<ReservationDto>>(reservations);
			return results;
		}
	}
}
