using FluentValidation;
using FluentValidation.Results;

namespace Lancamentos.Application.UseCases.Delete;

public class DeleteLancamentoValidator : AbstractValidator<DeleteLancamentoCommand>
{
    public override ValidationResult Validate(ValidationContext<DeleteLancamentoCommand> context)
    {
        RuleFor(x => x.LancamentoId)
            .NotEmpty()
            .WithMessage("Lançamento id é obrigatória");
        
        return base.Validate(context);
    }
}