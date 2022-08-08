namespace AthenaService.Domain.Models
{
    public class CurrentUserModel
    {
        public string? Ver { set; get; }
        public int UserId { set; get; }
        public int CurrentTenantId { set; get; }
        public string? Email { set; get; }
        public string? Name { set; get; }
        public string? GivenName { set; get; }
        public string? FamilyName { set; get; }
        public string? FirstName { set; get; }
        public string? LastName { set; get; }
        public List<string>? Roles { set; get; }
    }
}
