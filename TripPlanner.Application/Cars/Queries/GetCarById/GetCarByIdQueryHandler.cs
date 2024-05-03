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

namespace TripPlanner.Application.Cars.Queries.GetCarById
{
	public class GetCarByIdQueryHandler(ILogger<GetCarByIdQueryHandler> logger,
		IMapper mapper,
		IServiceRepository serviceRepository) : IRequestHandler<GetCarByIdQuery, CarDto>
	{
		public async Task<CarDto> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting car with id: {CarId} from service with id: {ServiceId}",
				request.CarId, request.CarId);

			var service = await serviceRepository.GetById(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}

			var car = service.Cars?.FirstOrDefault(c => c.Id == request.CarId);

			var result = mapper.Map<CarDto>(car);
			return result;
		}
	}
}
