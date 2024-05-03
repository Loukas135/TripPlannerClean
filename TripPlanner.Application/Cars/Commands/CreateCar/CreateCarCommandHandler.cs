using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Cars.Commands.CreateCar
{
	public class CreateCarCommandHandler(ILogger<CreateCarCommandHandler> logger,
		IMapper mapper,
		ICarRepository carRepository,
		IServiceRepository serviceRepository) : IRequestHandler<CreateCarCommand, int>
	{
		public async Task<int> Handle(CreateCarCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Creating new car in service with id: {ServiceId}, and category id: {CarCategoryId}"
				, request.ServiceId, request.CarCategoryId);
			var service = await serviceRepository.GetById(request.ServiceId);
			if (service == null) 
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}

			var car = mapper.Map<Car>(request);

			return await carRepository.Add(car);
		}
	}
}
