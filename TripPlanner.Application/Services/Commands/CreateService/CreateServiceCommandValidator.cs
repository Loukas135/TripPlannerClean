using FluentValidation;


namespace TripPlanner.Application.Services.Commands.CreateService
{
	public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
	{
		public CreateServiceCommandValidator() { }
	}
}
