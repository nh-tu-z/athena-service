using FluentValidation;

using AthenaService.Domain.Base;

using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.ViewModels
{
    public class EditPipelineSettingsInputModel
    {
        private string _pipelineName;
        public string PipelineName
        {
            get { return _pipelineName; }
            set { _pipelineName = value.Trim(); }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value.Trim(); }
        }
        public CriticalityEnums Criticality { get; set; }
    }

    public class EditPipelineSettingsInputModelValidator : AbstractValidator<EditPipelineSettingsInputModel>
    {
        public EditPipelineSettingsInputModelValidator()
        {
            // Validate for PipelineName
            RuleFor(x => x.PipelineName)
                .NotEmpty()
                .Length(CommonConstants.PipelineValidator.PipelineNameMinLength, CommonConstants.PipelineValidator.PipelineNameMaxLength);

            // Validate for Description
            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(CommonConstants.PipelineValidator.PipelineDescriptionMinLength, CommonConstants.PipelineValidator.PipelineDescriptionMaxLength);

            // Validate for Criticality
            RuleFor(x => x.Criticality).IsInEnum();
        }
    }
}
