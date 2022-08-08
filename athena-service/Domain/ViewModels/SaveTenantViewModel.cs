using AthenaService.Domain.Base;
using FluentValidation;

namespace AthenaService.Domain.ViewModels
{
    public class SaveTenantViewModel
    {
        public int TenantId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class SaveTenantViewModelValidator : AbstractValidator<SaveTenantViewModel>
    {
        public SaveTenantViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).Length(CommonConstants.NameMinLength, CommonConstants.NameMaxLength);
        }
    }
}
