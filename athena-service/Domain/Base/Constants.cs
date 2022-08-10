namespace AthenaService.Domain.Base
{
    public static class SortDirections
    {
        public const string Ascending = "asc";
        public const string Descending = "desc";
    }

    public class CommonConstants
    {
        public const string TenantHeaderKeyName = "tenantId";

        public const string TenantName = "Tenant Id ";

        public const int TenantAliasMaxLenght = 128;

        public static class PipelineValidator
        {
            public const int PipelineNameMaxLength = 255;
            public const int PipelineNameMinLength = 1;

            public const int PipelineDescriptionMaxLength = 500;
            public const int PipelineDescriptionMinLength = 1;

            public const int LastActivityMaxLength = 50;
            public const int LastActivityMinLength = 1;
        }

        public static class TagValidator
        {
            public const int TagNameMaxLength = 255;
            public const int TagNameMinLength = 1;
        }

        public static class Claims
        {
            public const string Id = "id";
            public const string Jti = "jti";
            public const string TenantId = "currentTenantId";
            public const string Ver = "ver";
            public const string UserId = "userId";
            public const string GivenName = "given_name";
            public const string FamilyName = "family_name";
            public const string FirstName = "firstName";
            public const string LastName = "lastName";
            public const string Email = "email";
            public const string Name = "name";
            public const string Roles = "roles";
        }

        public static class CollectorEvent
        {
            public const string Add = "Add";
            public const string Update = "Update";
            public const string Delete = "Delete";
        }

        public static class CollectorAssetType
        {
            public const string Model = "Model";
            public const string Dataset = "Dataset";
            public const string Experiment = "Experiment";
            public const string Endpoint = "Endpoint";
            public const string RunJob = "RunJob";
            public const string User = "User";
        }

        public static class Clams
        {
            public const string Email = "email";
            public const string Jti = "jti";
            public const string TenantIdClaimName = "currentTenantId";
        }

        public const int NameMaxLength = 255;
        public const int NameMinLength = 1;
        public const string DefaultTenantAlias = "tenant";
        public const string TenantMigrationDirectory = "Data/TenantDatabase/Migrations";
    }

    public static class Activities
    {
        public static class TenantActivities
        {
            public const string Created = "Tenant Created";
            public const string Updated = "Tenant Updated";

        }

        public static class UserActivities
        {
            public const string Created = "User Created";
            public const string Updated = "User Updated";

        }
    }

    public static class TenantMessages
    {
        public const string TenantNameIsExisted = "Tenant Name is existed.";
        public const string TenantIsNotFound = "Tenant is not found.";
        public const string TenantAliasIsNotGenerated = "Tenant Alias is not able to generated.";
    }
}
