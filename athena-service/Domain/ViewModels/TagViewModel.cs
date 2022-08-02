using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.ViewModels
{
    public class TagViewModel
    {
        public Guid TagId { get; set; }
        public TagCategoryEnums TagCategory { get; set; }
        public string TagName { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
    }
}
