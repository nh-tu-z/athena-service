namespace AthenaService.Common.CommandText
{
    public static class CommandAlertText
    {
		public const string GetAll = @"
			SELECT
				ar.[AlertRuleId],
				ar.[RuleName],
				ar.[Priority],
				ar.[CreatedOn],
				ar.[Status]
			FROM [AlertRule] ar
        ";
	}
}
