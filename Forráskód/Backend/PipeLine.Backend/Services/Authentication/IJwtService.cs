namespace PipeLine.Backend.Services.Authentication
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string email, bool stayLoggedIn);
    }
}
