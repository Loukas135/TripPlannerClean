using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Trips.Commands.CreateTrip;

namespace TripPlanner.Application.Trips.Validators
{
	public class CreateTripValidator : AbstractValidator<CreateTripCommand>
	{
	}
}
