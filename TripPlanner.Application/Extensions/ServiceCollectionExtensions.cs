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
using TripPlanner.Application.Rooms.Commands.CommandBehavior;
using TripPlanner.Application.Rooms.Commands.CreateRoom;
using TripPlanner.Application.Rooms.Commands.DeleteRoom;
using TripPlanner.Application.Trips.Commands.CommandBehavior;
using TripPlanner.Application.Trips.Commands.CreateTrip;
using TripPlanner.Application.Trips.Commands.DeleteTrip;
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

			services.AddTransient(typeof(IPipelineBehavior<CreateCarCommand,int>), typeof(CarCommandBehavior<CreateCarCommand,int>));
			services.AddTransient(typeof(IPipelineBehavior<DeleteCarCommand, Unit>), typeof(CarCommandBehavior<DeleteCarCommand, Unit>));

			services.AddTransient(typeof(IPipelineBehavior<CreateRoomCommand, int>), typeof(RoomCommandBehavior<CreateRoomCommand, int>));
			services.AddTransient(typeof(IPipelineBehavior<DeleteRoomCommand, Unit>), typeof(RoomCommandBehavior<DeleteRoomCommand, Unit>));

			services.AddTransient(typeof(IPipelineBehavior<CreateTripCommand, int>), typeof(TripCommandBehavior<CreateTripCommand, int>));
			services.AddTransient(typeof(IPipelineBehavior<DeleteTripCommand, Unit>), typeof(TripCommandBehavior<DeleteTripCommand, Unit>));
		}
	}
}
