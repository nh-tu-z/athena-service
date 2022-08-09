using FluentValidation;
using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.ViewModels
{
    public class AlertStatusInputModel
    {
        public AlertStatus NewStatus { get; set; }
        public AlertStatus CurrentStatus { get; set; }
    }

    public class AlertStatusInputModelValidator : AbstractValidator<AlertStatusInputModel>
    {
        public AlertStatusInputModelValidator()
        {
            RuleFor(x => x.NewStatus).IsInEnum();
            RuleFor(x => x.CurrentStatus).IsInEnum();
        }
    }
}
