using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Cars.Dtos;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Cars.Queries.GetAllCars
{
	public class GetAllCarsQueryHandler(ILogger<GetAllCarsQueryHandler> logger,
		IMapper mapper,
		IServiceRepository serviceRepository) : IRequestHandler<GetAllCarsQuery, IEnumerable<CarDto>>
	{
		public async Task<IEnumerable<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all cars from service with id: {ServiceId}", request.ServiceId);

			var service = await serviceRepository.GetById(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}

			var cars = mapper.Map<IEnumerable<CarDto>>(service.Cars);
			if (cars == null)
			{
				return null;
			}
			return cars;
		}
	}
}
