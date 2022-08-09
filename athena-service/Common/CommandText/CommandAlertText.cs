namespace AthenaService.Common.CommandText
{
    public class CommandAlertText
    {
        public const string GetAlertById = @"
			SELECT *
			FROM [Alert]
			WHERE [AlertId] = @id;";

        public const string UpdateAlertStatus = @"
            Update [Alert]
            Set [Status] = @Status
            Where [AlertId] = @AlertId
			AND [Version] = @Version;
            ";

		public const string GetAll = @"
            DECLARE @FirstRec INT, @LastRec INT
			SET @FirstRec = @PageNo * @PageSize + 1;
			SET @LastRec = (@PageNo + 1) * @PageSize;

			DECLARE @currentDateWithTimezone DATETIME, @currentDate DATETIME, @endOfCurrentDate DATETIME
			SET @currentDateWithTimezone = DATEADD(MINUTE, @TimezoneMinutes, DATEADD(HOUR, @TimezoneHours, GETUTCDATE()))
			SET @currentDate = DATEFROMPARTS(YEAR(@currentDateWithTimezone), MONTH(@currentDateWithTimezone), DAY(@currentDateWithTimezone))
			SET @endOfCurrentDate = DATEADD(SECOND, -1 , DATEADD(DAY, 1, @currentDate))

			SET @SearchValue = LTRIM(RTRIM(@SearchValue)) 
 
			; WITH CTE_Results AS  
			(  
				SELECT ROW_NUMBER() OVER (ORDER BY 
					CASE WHEN (@SortColumn = 1 AND @SortDirection = 'asc')  
						THEN Priority  
					END ASC,  
					CASE WHEN (@SortColumn = 1 AND @SortDirection = 'desc')  
						THEN Priority
					END DESC, 
					CASE WHEN (@SortColumn = 2 AND @SortDirection = 'asc')  
						THEN Title  
					END ASC,  
					CASE WHEN (@SortColumn = 2 AND @SortDirection = 'desc')  
						THEN Title  
					END DESC,
					CASE WHEN (@SortColumn = 3 AND @SortDirection = 'asc')  
						THEN Confidence  
					END ASC,  
					CASE WHEN (@SortColumn = 3 AND @SortDirection = 'desc')  
						THEN Confidence  
					END DESC,
					CASE WHEN (@SortColumn = 4 AND @SortDirection = 'asc')  
						THEN DetectedOn
					END ASC,  
					CASE WHEN (@SortColumn = 4 AND @SortDirection = 'desc')  
						THEN DetectedOn  
					END DESC,
					CASE WHEN (@SortColumn = 5 AND @SortDirection = 'asc')  
						THEN [Status]
					END ASC,  
					CASE WHEN (@SortColumn = 5 AND @SortDirection = 'desc')  
						THEN [Status]  
					END DESC
					) AS RowNum,
					COUNT(*) OVER() AS FilteredCount,
					a.AlertId,
					a.Priority,
					a.Title,
					a.Description,
					a.Confidence,
					a.DetectedOn,
					a.[Status]
				FROM Alert a 
				WHERE (ISNULL(@SearchValue, '') = '' OR LTRIM(a.Title)  LIKE '%' + @SearchValue + '%')
					AND (ISNULL(@IsFilteredByPriority, 0) = 0 OR a.Priority IN @Priority)
					AND (ISNULL(@IsFilteredByConfidence, 0) = 0 OR a.Confidence IN @Confidence)
					AND (@AlertRuleId IS NULL OR a.AlertRuleId = @AlertRuleId)
					AND (ISNULL(@IsFilteredByStatus, 0) = 0 OR a.Status IN @Status)
					AND (ISNULL(@DetectedOnRange, 0) = 0 OR DATEADD(MINUTE, @TimezoneMinutes, DATEADD(HOUR, @TimezoneHours, a.DetectedOn)) > (
						CASE 
							WHEN @DetectedOnRange = 1 THEN DATEADD(DAY, -7, @endOfCurrentDate)
							WHEN @DetectedOnRange = 2 THEN DATEADD(MONTH, -1, @endOfCurrentDate)
							WHEN @DetectedOnRange = 3 THEN DATEADD(MONTH, -3, @endOfCurrentDate)
							WHEN @DetectedOnRange = 4 THEN DATEADD(MONTH, -6, @endOfCurrentDate)
							WHEN @DetectedOnRange = 5 THEN DATEADD(YEAR, -1, @endOfCurrentDate)
						END))
			)

			SELECT
				rs.[AlertId],
				rs.[Priority],
				rs.[Title],
				rs.[Description],
				rs.[Confidence],
				rs.[DetectedOn],
				rs.[Status],
				FilteredCount
			FROM CTE_Results rs
			WHERE RowNum BETWEEN @FirstRec AND @LastRec
			ORDER BY RowNum            
        ";
	}
}
