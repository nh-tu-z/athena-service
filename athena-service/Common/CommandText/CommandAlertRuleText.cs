namespace AthenaService.Common.CommandText
{
    public static class CommandAlertRuleText
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
