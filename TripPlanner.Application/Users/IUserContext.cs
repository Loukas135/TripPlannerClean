namespace TripPlanner.Application.Users
{
	public interface IUserContext
	{
		CurrentUser? GetCurrentUser();
	}
}