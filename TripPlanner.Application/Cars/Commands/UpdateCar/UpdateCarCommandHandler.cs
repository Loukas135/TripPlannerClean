using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;
using TripPlanner.Domain.Entities.Service_Entities.Tourism_Office;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Cars.Commands.UpdateCar
{
	public class UpdateCarCommandHandler(IMapper mapper, ILogger<UpdateCarCommandHandler> logger,
		ICarRepository carRepository) : IRequestHandler<UpdateCarCommand>
	{
		public async Task Handle(UpdateCarCommand request, CancellationToken cancellationToken)
		{
            var car = await carRepository.GetById((int)(request.CarId));
			if (car == null)
			{
				throw new NotFoundException(nameof(Car), request.CarId.ToString());
			}

			mapper.Map(request, car);
			await carRepository.UpdateCar(request.ImagePath,car);
		}
	}
}
