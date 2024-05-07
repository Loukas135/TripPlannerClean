using Microsoft.OpenApi.Models;
using TripPlanner.API.Middlewares;
using TripPlanner.Domain.Entities;

namespace TripPlanner.API.Extensions
{
	public static class WebApplicationBuilderExtension
	{
		public static void AddPresentation(this WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<ExceptionHandlerMiddleware>();

			builder.Services.AddAuthentication();

			builder.Services.AddControllers();
			builder.Services.AddSwaggerGen(c =>
			{
				c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
						},
						[]
					}
				});
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddIdentityApiEndpoints<User>();
		}
	}
}
