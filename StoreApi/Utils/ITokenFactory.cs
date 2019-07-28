using Store.Core.Account;

namespace StoreApi.Utils
{
    public interface ITokenFactory
    {
        string UserIdClaim { get; }
        string RoleClaim { get; }
        string GenerateToken(string userId, Role role);
        string GetUser();
        string GetRole();
    }
}
