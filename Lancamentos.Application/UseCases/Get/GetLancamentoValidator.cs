using FluentValidation.Results;

namespace Lancamentos.Application.UseCases.Get;

using FluentValidation;

public class GetLancamentoValidator : AbstractValidator<GetLancamentoCommand>
{
    public override ValidationResult Validate(ValidationContext<GetLancamentoCommand> context)
    {
        RuleFor(x => x.LancamentoId)
            .NotEmpty()
            .WithMessage("Lancamento Id é obrigatória");
        
        return base.Validate(context);
    }
}