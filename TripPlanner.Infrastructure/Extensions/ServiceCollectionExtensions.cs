using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;
using TripPlanner.Infrastructure.Repositories;
using TripPlanner.Infrastructure.Seeders.CarCategories;
using TripPlanner.Infrastructure.Seeders.Governorates;
using TripPlanner.Infrastructure.Seeders.RolesSeeder;
using TripPlanner.Infrastructure.Seeders.RoomCategories;
using TripPlanner.Infrastructure.Seeders.ServiceTypeSeeder;
namespace TripPlanner.Infrastructure.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("TripPlannerCleanDb");
			services.AddDbContext<TripPlannerDbContext>(options => options.UseSqlServer(connectionString));

			services.AddIdentityCore<User>()
			.AddRoles<IdentityRole>()
			.AddTokenProvider<DataProtectorTokenProvider<User>>("TripPlannerTokenProvidor")
			.AddEntityFrameworkStores<TripPlannerDbContext>()
			.AddDefaultTokenProviders();

			services.AddScoped<IGovernorateSeeder, GovernorateSeeder>();
			services.AddScoped<IServiceTypeSeeder, ServiceTypeSeeder>();
			services.AddScoped<IRoomCategorySeeder, RoomCategorySeeder>();
			services.AddScoped<ICarCategorySeeder, CarCategorySeeder>();
			services.AddScoped<IRolesSeeder, RolesSeeder>();
			//-----------------------------------------------------------
			services.AddScoped<IServiceRepository, ServiceRepository>();
			services.AddScoped<IGovernoratesRepository, GovernoratesRepository>();
			services.AddScoped<IRoomRepository, RoomRepository>();
			services.AddScoped<ICarRepository, CarRepository>();
			services.AddScoped<ITripRepository, TripRepository>();
			services.AddScoped<ITokenRepository, TokenRepository>();
			services.AddScoped<IAccountRepository, AccountRepository>();
			services.AddScoped(typeof(ISeededValuesRepository<>),typeof(SeededValuesRepository<>));
			services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IServicetypeRepository, ServicetypeRepository>();
			services.AddScoped<IRatingRepository, RatingRepository>();
			services.AddScoped<IReservationRespository, ReservationRespository>();
		}
	}
}
