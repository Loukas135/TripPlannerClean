using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Cars.Commands.DeleteCar
{
	public class DeleteCarCommandHandler(ILogger<DeleteCarCommandHandler> logger,
		ICarRepository carRepository,
		IServiceRepository serviceRepository,
		IReservationRespository reservationRespository) : IRequestHandler<DeleteCarCommand, Unit>
	{
		public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Deleting car with id: {CarId} from service with id: {ServiceId}",
				request.CarId, request.ServiceId);

			var service = await serviceRepository.GetById(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}

			var car = service.Cars?.FirstOrDefault(c => c.Id == request.CarId);
            var carReservations = await reservationRespository.GetBySubServiceId(request.CarId);
            foreach (var reservation in carReservations)
            {
                await reservationRespository.DeleteReservation(reservation);
            }
            await carRepository.Delete(car!);

			return Unit.Value;
		}
	}
}
