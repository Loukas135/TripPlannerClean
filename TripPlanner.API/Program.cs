using TripPlanner.API.Extensions;
using TripPlanner.API.Middlewares;
using TripPlanner.Application.Extensions;
using TripPlanner.Domain.Entities;
using TripPlanner.Infrastructure.Extensions;
using TripPlanner.Infrastructure.Seeders.CarCategories;
using TripPlanner.Infrastructure.Seeders.Governorates;
using TripPlanner.Infrastructure.Seeders.ReservationsStatus;
using TripPlanner.Infrastructure.Seeders.RolesSeeder;
using TripPlanner.Infrastructure.Seeders.RoomCategories;
using TripPlanner.Infrastructure.Seeders.ServiceTypeSeeder;
using TripPlanner.Infrastructure.Seeders.Users;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentation();
builder.Services.AddApplicaiton();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod());
});
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

var userSeeder = scope.ServiceProvider.GetRequiredService<IUserSeeder>();
await userSeeder.Seed();

var statusSeeder = scope.ServiceProvider.GetRequiredService<IStatusSeeder>();
await statusSeeder.Seed();
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlerMiddleware>();

//if (app.Environment.IsDevelopment())
//{
	app.UseSwagger();
	app.UseSwaggerUI();
//}
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});
app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
