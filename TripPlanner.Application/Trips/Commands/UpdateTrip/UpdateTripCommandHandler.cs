using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;
using TripPlanner.Domain.Entities.Service_Entities.Tourism_Office;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Trips.Commands.UpdateTrip
{
	public class UpdateTripCommandHandler(IMapper mapper, ILogger<UpdateTripCommandHandler> logger,
		ITripRepository tripRepository) : IRequestHandler<UpdateTripCommand>
	{
		public async Task Handle(UpdateTripCommand request, CancellationToken cancellationToken)
		{
			var trip = await tripRepository.GetById(request.TripId);
			if (trip == null)
			{
				throw new NotFoundException(nameof(Trip), request.TripId.ToString());
			}

			mapper.Map(request, trip);
			await tripRepository.UpdateTrip(request.TripImage!,trip);
		}
	}
}
