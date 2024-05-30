using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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

			builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
					ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
					ValidAudience = builder.Configuration["JwtSettings:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
				};
				}
            );

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
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod());
            });

            builder.Services.AddIdentityApiEndpoints<User>();
		}
	}
}
