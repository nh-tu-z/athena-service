namespace AthenaService.Common.CommandText
{
    public static class CommandTenantDatabaseSchemaText
    {
		private const string GET_TENANT_DB_SCHEMA_SELECT_FROM = @"SELECT s.[TenantDatabaseSchemaId], s.[DatabaseName], s.[SchemaName], s.[State]
				, t.[TenantId], t.[TenantAlias], t.[Name], t.[State]
				FROM [TenantDatabaseSchema] s
				JOIN [Tenant] t on t.TenantId = s.TenantId
				";

		public static string GetTenantDatabaseSchema
			=> GET_TENANT_DB_SCHEMA_SELECT_FROM +
				"WHERE s.[TenantDatabaseSchemaId] = @TenantDatabaseSchemaId;";

		public static string GetTenantDatabaseSchemaByTenantIdAndDatabaseName
			=> GET_TENANT_DB_SCHEMA_SELECT_FROM +
				"WHERE t.[TenantId] = @TenantId AND s.[DatabaseName] = @DatabaseName;";

		public static string InsertTenantDatabaseSchema
			=> @"INSERT INTO TenantDatabaseSchema ([TenantId], [DatabaseName], [SchemaName], [State]) 
				OUTPUT INSERTED.TenantDatabaseSchemaId 
				VALUES (@TenantId, @DatabaseName, @SchemaName, @State)";

		public static string UpdateTenantDatabaseSchemaState
			=> @"UPDATE TenantDatabaseSchema SET [State] = @State
				OUTPUT INSERTED.TenantDatabaseSchemaId
				WHERE TenantDatabaseSchemaId = @TenantDatabaseSchemaId";
	}
}
