using Corporate.Contta.Schedule.Application.Mapping.Request;
using Corporate.Contta.Schedule.Infra.Repositories;
using FluentValidation;
using System;

namespace Corporate.Contta.Schedule.Api.Validators
{
    public class FaturamentoValidator : AbstractValidator<NewFaturamento12Request>
    {
        private FaturamentoEmpresaRepository _faturamentoEmpresaRepository;

        public FaturamentoValidator()
        {
            CompanyInformationValidate();
            NaoExisteLancamento();
            PossuiLancamentos();
        }

        private void CompanyInformationValidate()
        {
            RuleFor(c => c.CompanyInformation)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("Identificação da empresa inválido. Por favor verificar os dados informados!");
        }

        private void NaoExisteLancamento()
        {
            RuleFor(x => x.CompanyInformation)
                .Must((x, y) => NaoPossuiLancamentoParaEmpresa(x.CompanyInformation))
                .When(x => x.Faturamentos != null && x.Faturamentos.Count > 0)
                .WithMessage("Já existe faturamentos lançados para essa empresa.");
        }

        private void PossuiLancamentos()
        {
            RuleFor(x => x.Faturamentos)
                .NotNull()
                .Must((x, y) => x.Faturamentos.Count > 0)
                .WithMessage("Faturamentos não podem estar vazio.");
        }

        private bool NaoPossuiLancamentoParaEmpresa(Guid companyInformation)
        {
            return !FaturamentoEmpresaRepository.ExistsFaturamentoToCompany(companyInformation);
        }

        private FaturamentoEmpresaRepository FaturamentoEmpresaRepository
        {
            get
            {
                if (_faturamentoEmpresaRepository == null)
                {
                    _faturamentoEmpresaRepository = new FaturamentoEmpresaRepository();
                }

                return _faturamentoEmpresaRepository;
            }
        }
    }
}
