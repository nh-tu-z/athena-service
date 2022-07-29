namespace AthenaService.Common.CommandText.Pipeline
{
    public static class CommandPipelineText
    {
        public const string GetById = @"SELECT * FROM Pipeline WHERE PipelineId = @id";
    }
}
