using Corporate.Contta.Schedule.Application.Mapping.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Api.Validators
{
    public class CompanyValidator : AbstractValidator<NewCompanyRequest>
    {
        public CompanyValidator()
        {
            NameValidate();
            //NameFantasyValidate();
            //EmailValidate();
            CnpjValidate();
            PhoneValidate();
        }

        private void NameValidate()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .Length(5, 250)
                .WithMessage("Nome inválido. Por favor verificar os dados informados!");
        }
        private void NameFantasyValidate()
        {
            RuleFor(c => c.NameFantasy)
                .NotEmpty()
                .NotNull()
                .Length(10, 120)
                .WithMessage("Nome fantasia inválido. Por favor verificar os dados informados!");
        }
        private void PhoneValidate()
        {
            RuleFor(c => c.Phone)
                .NotEmpty()
                .NotNull()
                .Length(10, 20)
                .WithMessage("Nº de Telefone inválido. Por favor verificar os dados informados!");
        }
        private void EmailValidate()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .Length(10, 120)
                .WithMessage("E-mail inválido. Por favor verificar os dados informados!");
        }
        private void CnpjValidate()
        {
            RuleFor(c => c.Cnpj)
                .NotEmpty()
                .NotNull()
                .Length(14, 18)
                .WithMessage("Cnpj inválido. Por favor verificar os dados informados!");
        }
    }
}
