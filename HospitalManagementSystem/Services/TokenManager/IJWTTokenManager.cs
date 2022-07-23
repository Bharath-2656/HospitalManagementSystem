using HospitalManagementSystem.Model;

namespace HospitalManagementSystem.Services.TokenManager
{
    public interface IJWTTokenManager
    {
        Tokens Authenticate(string Username, string Role);
    }
}
