namespace AthenaService.Common.CommandText
{
    public static class CommandTenantProvisionTaskText
    {
        public static string InsertTenantProvisionTask
            => @"INSERT INTO TenantProvisionTask ([TenantId], [State], [ErrorMessage]) 
				OUTPUT INSERTED.TenantProvisionTaskId 
				VALUES (@TenantId, @State, @ErrorMessage)";
    }
}
