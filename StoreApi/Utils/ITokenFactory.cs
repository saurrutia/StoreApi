using Store.Core.Account;

namespace StoreApi.Utils
{
    public interface ITokenFactory
    {
        string UserIdClaim { get; }
        string RoleIdClaim { get; }
        string GenerateToken(string userId, Role role);
    }
}
