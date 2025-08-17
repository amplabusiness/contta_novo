using Corporate.Contta.Schedule.Application.Mapping.Request;
using FluentValidation;

namespace Corporate.Contta.Schedule.Api.Validators
{
    public class GetInfomationByDocumentValidator : AbstractValidator<GetInfomationByDocumentRequest>
    {
        public GetInfomationByDocumentValidator()
        {
            DocumentValidate();
        }

        void DocumentValidate()
        {
            RuleFor(c => c.Document)
               .NotEmpty()
               .NotNull()
               .Length(11, 14)
               .WithMessage("Documento inválido. Por favor verificar os dados informados!");
        }
    }
}
