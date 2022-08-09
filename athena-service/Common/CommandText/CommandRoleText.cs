using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Common.CommandText
{
    public static class CommandRoleText
    {
		public static string CreateRolesForTenant = $@"
				INSERT INTO [RoleTenant]
				SELECT @TenantId, [RoleId], [State]
				FROM [Role]
				WHERE [Type] = {(int)RoleType.User}
				AND [State] <> {(int)RoleState.Disabled}
				;";
	}
}
