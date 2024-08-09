using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Entities.Service_Entities.Tourism_Office;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Trips.Commands.CreateTrip
{
	public class CreateTripCommandHandler(ILogger<CreateTripCommandHandler> logger,
		IServiceRepository serviceRepository,
		ITripRepository tripRepository,
		IMapper mapper) : IRequestHandler<CreateTripCommand, int>
	{
		public async Task<int> Handle(CreateTripCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Creating new Trip in service with id: {ServiceId}", request.ServiceId);

			var service = await serviceRepository.GetById(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}
			var imagePath = await tripRepository.SaveTripImageAsync(request.TripImage);
			var trip = mapper.Map<Trip>(request);
			trip.ImagePath = imagePath;
			trip.From = request.From;
			trip.To = request.To;
			return await tripRepository.Add(trip);
		}
	}
}
