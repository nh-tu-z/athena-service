namespace AthenaService.Domain.Base
{
    public class Enums
    {
        public enum CriticalityEnums : int
        {
            Low = 1,
            Moderate = 2,
            High = 3
        }

        public enum StateEnums : int
        {
            Draft = 1,
            NotAssessed = 2,
            InTraining = 3,
            PreProduction = 4,
            Production = 5
        }

        public enum RangeDate : int
        {
            Last7Days = 1,
            Last1Month = 2,
            Last3Months = 3,
            Last6Months = 4,
            Last1Year = 5
        }

        public enum PipelineSortableColumn : int
        {
            SecurityScore = 1,
            PipelineName = 2,
            Assets = 3,
            Criticality = 4,
            LastActivityTimeStamp = 5,
            State = 6,
            CreateAt = 7
        }

        public enum TagCategoryEnums : int
        {
            BusinessFunction = 1,
            BusinessProject = 2
        }

        public enum MessageType
        {
            Integration = 1,
        }

        public enum IntegrationState : int
        {
            Connected = 1,
            Disconnected = 2,
            Disabled = 3,
        }

        public enum EnvironmentType : int
        {
            Azure = 1,
            AWS = 2,
            Databricks = 3,
        }

        public enum IntegrationActionType
        {
            Enable = 1,
            Disable = 2,
            CheckConnection = 3,
            SyncIntegration = 4
        }
    }
}
