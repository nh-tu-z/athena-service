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

        public enum TenantState : int
        {
            Active = 1,
            Disabled = 2,
            SetupInProgress = 3,
            SetupFailed = 4,
            AwaitingSetup = 5,
        }

        public enum UserState : int
        {
            Active = 1,
            Disabled = 2,
            Invited = 3,
        }

        public enum TenantProvisionTaskState : int
        {
            Running = 1,
            Success = 2,
            Failed = 3
        }

        public enum TenantDatabaseSchemaState : int
        {
            NotReady = 1,
            Provisioned = 2,
            ProvisionedFailed = 3
        }

        public enum RoleType : int
        {
            Admin = 1,
            User = 2,
        }

        public enum RoleState : int
        {
            Required = 1,
            Enabled = 2,
            Disabled = 3,
        }

        public enum AlertPriority : int
        {
            Low = 1,
            Moderate = 2,
            High = 3
        }

        public enum AlertRuleStatus : int
        {
            RunRunning = 1,
            Paused = 2
        }

        public enum AlertConfidence : int
        {
            Low = 1,
            Moderate = 2,
            High = 3
        }

        public enum AlertStatus : int
        {
            New = 1,
            Active = 2,
            Dismissed = 3,
            Resolved = 4,
            Escalated = 5
        }

        public enum AlertSortableColumn : int
        {
            Priority = 1,
            Title = 2,
            Confidence = 3,
            DetectedOn = 4,
            Status = 5
        }

        public enum PermissionType : int
        {
            Admin = 1,
            User = 2,
        }
    }
}
