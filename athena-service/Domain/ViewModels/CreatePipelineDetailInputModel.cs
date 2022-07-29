using FluentValidation;

using AthenaService.Domain.Base;

using static AthenaService.Domain.Base.Enums;

namespace AthenaService.Domain.ViewModels
{
    public class CreatePipelineDetailInputModel
    {
        private string _pipelineName;
        private string _description;

        public string PipelineName
        {
            get { return _pipelineName; }
            set { _pipelineName = value.Trim(); }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value.Trim(); }
        }
        public int SecurityScore { get; set; } = 0;
        public CriticalityEnums Criticality { get; set; }
        public StateEnums State { get; set; } = StateEnums.NotAssessed;
        public string LastActivity { get; set; } = string.Empty;
        public DateTime LastActivityTimeStamp { get; set; }
    }

    public class CreatePipelineDetailModelValidator : AbstractValidator<CreatePipelineDetailInputModel>
    {
        public CreatePipelineDetailModelValidator()
        {
            // Validate for PipelineName
            RuleFor(x => x.PipelineName)
                .NotEmpty()
                .Length(CommonConstants.PipelineValidator.PipelineNameMinLength, CommonConstants.PipelineValidator.PipelineNameMaxLength);

            // Validate for Description
            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(CommonConstants.PipelineValidator.PipelineDescriptionMinLength, CommonConstants.PipelineValidator.PipelineDescriptionMaxLength);

            // Validate for SecurityScore
            RuleFor(x => x.SecurityScore)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            // Validate for Criticality
            RuleFor(x => x.Criticality).IsInEnum();

            // Validate for State
            RuleFor(x => x.State).IsInEnum();
        }
    }
}

