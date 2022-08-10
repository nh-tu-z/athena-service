using AthenaService.Domain.Models;

namespace AthenaService.Interfaces
{
    public interface ICurrentUser
    {
        CurrentUserModel GetCurrentUser();
        string? GetClaimByType(string type);
    }
}
