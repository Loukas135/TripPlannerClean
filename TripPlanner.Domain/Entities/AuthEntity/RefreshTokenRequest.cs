namespace TripPlanner.Domain.Entities.AuthEntity
{
    public class RefreshTokenRequest
    {
        public string? RefreshToken { get; set; }
      public  string? user_id { get; set; }
    }
}
