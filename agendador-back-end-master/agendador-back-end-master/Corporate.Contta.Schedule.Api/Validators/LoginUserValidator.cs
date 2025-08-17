using Corporate.Contta.Schedule.Application.Mapping.Param;
using FluentValidation;

namespace Corporate.Contta.Schedule.Api.Validators
{
    public class LoginUserValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserValidator()
        {
            EmailValidate();
            PasswordValidate();
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
     
        private void PasswordValidate()
        {
            RuleFor(c => c.Password)
                .NotEmpty()
                .NotNull()
                .Length(6, 12)
                .WithMessage("Senha inválida. Por favor verificar os dados informados!");
        }
    }
}
