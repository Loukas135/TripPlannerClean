
using Microsoft.Extensions.Logging;
using TripPlanner.Domain.Exceptions;

namespace TripPlanner.API.Middlewares
{
	public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next.Invoke(context);
			}
			catch (NotFoundException notFound)
			{
				logger.LogWarning(notFound.Message);

				context.Response.StatusCode = 404;
				await context.Response.WriteAsync(notFound.Message);
			}
			catch (NoBalanceException noBalance)
			{
				logger.LogWarning($"No balance: {noBalance.Message}");

				context.Response.StatusCode = 400;
				await context.Response.WriteAsync(noBalance.Message);
			}
			catch (NoResourceAvailable noResource)
			{
				logger.LogWarning(noResource.Message);

				context.Response.StatusCode = 400;
				await context.Response.WriteAsync($"Quantity is 0: {noResource.Message}");
			}
			catch (OwnershipException violation)
			{
				logger.LogWarning(violation.Message);

				context.Response.StatusCode = 401;
				await context.Response.WriteAsync($"Editing in a service that isn't yours");
			}
			catch (ServiceTypeException ste)
			{
				logger.LogWarning(ste.Message);
				context.Response.StatusCode = 400;
			}
			catch(UserAlreadyExistsException ex)
			{
				logger.LogWarning(ex.Message);
				context.Response.StatusCode = 400;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);

				context.Response.StatusCode = 500;
				await context.Response.WriteAsync("Something went wrong");
			}

		}
	}
}
