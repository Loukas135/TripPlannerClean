using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Cars.Commands.CreateCar;

namespace TripPlanner.Application.Cars.Valdators
{
	public class CreateCarValidator : AbstractValidator<CreateCarCommand>
	{
	}
}
