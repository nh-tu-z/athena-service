namespace AthenaService.Domain.Base
{
    public class BaseEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class FullBaseEntity : BaseEntity
    {
        public string LastActivity { get; set; } = string.Empty;
        public DateTime? LastActivityDate { get; set; }
    }

}