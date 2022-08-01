namespace AthenaService.Common.CommandText.Pipeline
{
    public static class CommandPipelineText
    {
        public const string GetById = @"SELECT * FROM Pipeline WHERE PipelineId = @id";

        public const string InsertPipeline = @"
			SET @PipelineName = LTRIM(RTRIM(@PipelineName));
			SET @Description = LTRIM(RTRIM(@Description));

			INSERT INTO Pipeline ([PipelineName], [SecurityScore], [Description], [Criticality], [State], [LastActivity], [LastActivityTimeStamp], [CreatedAt], [UpdatedAt]) 
			OUTPUT INSERTED.[PipelineId] 
			VALUES (@PipelineName, @SecurityScore, @Description, @Criticality, @State, @LastActivity, @LastActivityTimeStamp, @CreatedAt, @UpdatedAt)";
    }
}
