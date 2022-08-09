using FluentValidation;

namespace AthenaService.Domain.ViewModels
{
    public class CreateTenantProvisionTaskViewModel
    {
        public int TenantId { get; set; }
    }

    public class CreateTenantProvisionTaskViewModelValidator : AbstractValidator<CreateTenantProvisionTaskViewModel>
    {
        public CreateTenantProvisionTaskViewModelValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty();
        }
    }
}
