using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;
using TripPlanner.Infrastructure.Repositories;
using TripPlanner.Infrastructure.Seeders.CarCategories;
using TripPlanner.Infrastructure.Seeders.Governorates;
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

			services.AddScoped<IGovernorateSeeder, GovernorateSeeder>();
			services.AddScoped<IServiceTypeSeeder, ServiceTypeSeeder>();
			services.AddScoped<IRoomCategorySeeder, RoomCategorySeeder>();
			services.AddScoped<ICarCategorySeeder, CarCategorySeeder>();

			services.AddScoped<IServiceRepository, ServiceRepository>();
			services.AddScoped<IGovernoratesRepository, GovernoratesRepository>();
			services.AddScoped<IRoomRepository, RoomRepository>();
			services.AddScoped<ICarRepository, CarRepository>();
		}
	}
}
