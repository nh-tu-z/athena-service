namespace AthenaService.Interfaces
{
    public interface IRoleService
    {
        Task CreateRolesForTenant(int tenantid);
    }
}
