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

namespace TripPlanner.Application.Trips.Commands.DeleteTrip
{
	public class DeleteTripCommandHandler(ILogger<DeleteTripCommandHandler> logger,
		IServiceRepository serviceRepository,
		ITripRepository tripRepository) : IRequestHandler<DeleteTripCommand, Unit>
	{
		public async Task<Unit> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Deleting trip with id: {TripId} from service with id: {ServiceId}",
				request.TripId, request.ServiceId);

			var service = await serviceRepository.GetById(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}

			var trip = service.Trips?.FirstOrDefault(c => c.Id == request.TripId);
			await tripRepository.Delete(trip!);

			return Unit.Value;
		}
	}
}
