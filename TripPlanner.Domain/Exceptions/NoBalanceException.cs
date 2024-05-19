namespace TripPlanner.Domain.Exceptions
{
	public class NoBalanceException(string message) : Exception($"{message}")
	{
	}
}
