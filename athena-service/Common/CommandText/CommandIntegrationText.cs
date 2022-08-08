namespace AthenaService.Common.CommandText
{
    public class CommandIntegrationText
    {
        public const string GetById = @"SELECT * FROM Integration WHERE IntegrationId = @id";

		public const string GetAll = @"
			DECLARE @FirstRec INT, @LastRec INT
			SET @FirstRec = @PageNo * @PageSize + 1;
			SET @LastRec = (@PageNo + 1) * @PageSize;
 
			; WITH CTE_Results AS  
			(  
			SELECT ROW_NUMBER() OVER (ORDER BY 
			CASE WHEN (@SortColumn = 1 AND @SortDirection = 'asc')  
				THEN IntegrationName  
			END ASC,  
			CASE WHEN (@SortColumn = 1 AND @SortDirection = 'desc')  
				THEN IntegrationName
			END DESC, 
			CASE WHEN (@SortColumn = 2 AND @SortDirection = 'asc')  
				THEN i.[Description]  
			END ASC,  
			CASE WHEN (@SortColumn = 2 AND @SortDirection = 'desc')  
				THEN i.[Description]  
			END DESC,
			CASE WHEN (@SortColumn = 3 AND @SortDirection = 'asc')  
				THEN env.[Description]  
			END ASC,  
			CASE WHEN (@SortColumn = 3 AND @SortDirection = 'desc')  
				THEN env.[Description]  
			END DESC,
			CASE WHEN (@SortColumn = 4 AND @SortDirection = 'asc')  
				THEN stt.[Description]
			END ASC,  
			CASE WHEN (@SortColumn = 4 AND @SortDirection = 'desc')  
				THEN stt.[Description]  
			END DESC
			) AS RowNum,
			COUNT(*) OVER() AS FilteredCount,
			i.IntegrationId,
			i.IntegrationName,
			i.Environment,
			i.[Description],
			i.[State]
			FROM Integration i
			JOIN EnumDetail stt ON stt.[Value] = i.[State] AND stt.EnumTypeId = 1 -- EnumTypeId = 1 is IntegrationState 
			JOIN EnumDetail env ON env.[Value] = i.Environment AND env.EnumTypeId = 2 -- EnumTypeId = 2 is EnvironmentType 
			) SELECT
				rs.IntegrationId, 
				rs.IntegrationName, 
				rs.Environment,
				rs.[Description],
				rs.[State],
				FilteredCount
			FROM CTE_Results rs
			WHERE RowNum BETWEEN @FirstRec AND @LastRec
			ORDER BY RowNum";

		public const string GetAllIntegrationName = @"SELECT DISTINCT i.[IntegrationName] FROM Integration i";

		public const string UpdateIntegrationStateByTokenId = @"UPDATE [Integration] SET [State] = @state, [StateUpdatedAt] = @stateUpdatedAt
																WHERE [TokenId] = @tokenId";
	}
}
