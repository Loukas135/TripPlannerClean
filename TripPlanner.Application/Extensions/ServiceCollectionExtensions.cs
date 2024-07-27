using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Cars.Commands.CommandBehavior;
using TripPlanner.Application.Cars.Commands.CreateCar;
using TripPlanner.Application.Cars.Commands.DeleteCar;
using TripPlanner.Application.Users;

namespace TripPlanner.Application.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddApplicaiton(this IServiceCollection services)
		{
			var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;


			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
			services.AddSignalR();

			services.AddAutoMapper(applicationAssembly);

			services.AddValidatorsFromAssembly(applicationAssembly)
				.AddFluentValidationAutoValidation();

			services.AddScoped<IUserContext, UserContext>();
		}
	}
}
