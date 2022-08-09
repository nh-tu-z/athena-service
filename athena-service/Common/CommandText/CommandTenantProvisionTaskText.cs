namespace AthenaService.Common.CommandText
{
    public static class CommandTenantProvisionTaskText
    {
        public static string InsertTenantProvisionTask
            => @"INSERT INTO TenantProvisionTask ([TenantId], [State], [ErrorMessage]) 
				OUTPUT INSERTED.TenantProvisionTaskId 
				VALUES (@TenantId, @State, @ErrorMessage)";

		public static string GetTenantProvisionTaskById
			=> @"SELECT task.[TenantProvisionTaskId], task.[State], task.[ErrorMessage]
				, tenant.TenantId, tenant.TenantAlias, tenant.Name, tenant.State
				FROM [TenantProvisionTask] task
				JOIN [Tenant] tenant on tenant.TenantId = task.TenantId
				WHERE task.[TenantProvisionTaskId] = @TenantProvisionTaskId";

		public static string UpdateTenantProvisionTask
			=> @"UPDATE TenantProvisionTask SET [State] = @State, [ErrorMessage] = @ErrorMessage
				WHERE TenantProvisionTaskId = @TenantProvisionTaskId";
	}
}
