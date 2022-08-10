namespace AthenaService.Common.CommandText
{
    public static class CommandPermissionText
    {
        public const string GetPermissionsByType = @"
				SELECT p.[PermissionId], p.[Type], p.[Module], p.[Function], p.[Name], p.PermissionCode
				FROM [dbo].[Permission] AS p
				WHERE p.[Type] = @Type";

		public const string GetAdminUserPermissions = @"
				SELECT
					ur.UserId,
					p.PermissionId, 
					p.[Type], 
					p.[Module], 
					p.[Function], 
					p.[Name], 
					p.PermissionCode
				FROM [UserRole] ur
				JOIN [User] u ON ur.UserId = u.UserId
				JOIN [Role] r ON ur.RoleId = r.RoleId
				JOIN RolePermission rp ON r.RoleId = rp.RoleId
				JOIN [Permission] p ON rp.PermissionId = p.PermissionId AND r.[Type] = p.[Type]
				WHERE ur.UserId = @userId 
					AND r.[Type] IN @roleTypes 
					AND r.State <> 3
					AND u.State = 1";

		public const string GetTenantUserPermissions = @"
				SELECT DISTINCT
					ur.UserId,
					p.PermissionId, 
					p.[Type], 
					p.[Module], 
					p.[Function], 
					p.[Name], 
					p.PermissionCode
				FROM [UserRole] ur
				JOIN [User] u ON ur.UserId = u.UserId
				JOIN Tenant t ON ur.TenantId = t.TenantId
				JOIN [RoleTenant] r ON ur.RoleId = r.RoleId
				JOIN RolePermission rp ON r.RoleId = rp.RoleId
				JOIN [Permission] p ON rp.PermissionId = p.PermissionId AND rp.PermissionId = p.PermissionId
				WHERE ur.UserId = @userId 
					AND r.State <> 3
					AND u.State = 1
					AND ur.TenantId = @tenantId";
	}
}
