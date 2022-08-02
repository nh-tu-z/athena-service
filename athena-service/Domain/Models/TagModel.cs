using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.Models
{
    public class TagModel
    {
        public Guid TagId { get; set; }
        public TagCategoryEnums TagCategory { get; set; }
        public string TagName { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
    }
}
