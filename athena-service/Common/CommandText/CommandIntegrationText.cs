namespace AthenaService.Common.CommandText
{
    public class CommandIntegrationText
    {
        public const string GetById = @"SELECT * FROM Integration WHERE IntegrationId = @id";
    }
}
