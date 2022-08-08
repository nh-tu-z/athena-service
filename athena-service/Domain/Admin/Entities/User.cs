using Dapper.Contrib.Extensions;
using AthenaService.Domain.Base;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.Admin.Entities
{
    [Table("[User]")]
    public class User : FullBaseEntity
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserState State { get; set; }
        public Guid? Jti { get; set; }
    }
}
