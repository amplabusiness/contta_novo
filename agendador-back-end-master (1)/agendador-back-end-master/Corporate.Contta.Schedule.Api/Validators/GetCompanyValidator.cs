using Corporate.Contta.Schedule.Application.Mapping.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Api.Validators
{
    public class GetCompanyValidator:AbstractValidator<GetCompanyRequest>
    {
        public GetCompanyValidator()
        {
            DocumentValidate();
        }

        void DocumentValidate()
        {
            RuleFor(c => c.Document)
               .NotEmpty()
               .NotNull()
               .Length(14, 18)
               .WithMessage("Cnpj inválido. Por favor verificar os dados informados!");
        }
    }
}
