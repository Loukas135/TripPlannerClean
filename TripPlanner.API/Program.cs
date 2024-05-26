using TripPlanner.API.Extensions;
using TripPlanner.API.Middlewares;
using TripPlanner.Application.Extensions;
using TripPlanner.Domain.Entities;
using TripPlanner.Infrastructure.Extensions;
using TripPlanner.Infrastructure.Seeders.CarCategories;
using TripPlanner.Infrastructure.Seeders.Governorates;
using TripPlanner.Infrastructure.Seeders.RolesSeeder;
using TripPlanner.Infrastructure.Seeders.RoomCategories;
using TripPlanner.Infrastructure.Seeders.ServiceTypeSeeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentation();
builder.Services.AddApplicaiton();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

var scope = app.Services.CreateScope();

var govSeeder = scope.ServiceProvider.GetRequiredService<IGovernorateSeeder>();
await govSeeder.Seed();

var stSeeder = scope.ServiceProvider.GetRequiredService<IServiceTypeSeeder>();
await stSeeder.Seed();

var carSeeder = scope.ServiceProvider.GetRequiredService<ICarCategorySeeder>();
await carSeeder.Seed();

var roomSeeder = scope.ServiceProvider.GetRequiredService<IRoomCategorySeeder>();
await roomSeeder.Seed();

var roleSeeder = scope.ServiceProvider.GetRequiredService<IRolesSeeder>();
await roleSeeder.Seed();
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();

app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();

app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
