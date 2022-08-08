namespace AthenaService.Common.CommandText.Tenant
{
    public static class CommandTenantText
    {
        public const string GetById = @"SELECT * FROM Tenant WHERE TenantId = @id";

        public const string GetTenant = @"SELECT TOP 1 t.TenantId, t.TenantAlias, t.[Name], t.[State], t.CreatedAt, t.LastActivity, t.LastActivityDate, u.UserId, u.FirstName, u.LastName
                                            FROM Tenant t
                                            JOIN[User] u on t.CreatedBy = u.UserId 
                                            WHERE TenantId = @TenantId";

        public const string CheckExistedTenantName = @"SELECT 1 FROM Tenant
                                                        WHERE [Name] = @Name 
	                                                        AND (CASE
		                                                        WHEN @TenantId IS NULL THEN 1
		                                                        ELSE CASE
				                                                        WHEN TenantId <> @TenantId THEN 1
				                                                        ELSE 0
			                                                            END
	                                                        END) = 1";
    }
}
