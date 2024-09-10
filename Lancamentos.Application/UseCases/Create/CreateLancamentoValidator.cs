using System.Data;
using FluentValidation.Results;

namespace Lancamentos.Application.UseCases.Create;

using FluentValidation;

public class CreateLancamentoValidator : AbstractValidator<CreateLancamentoCommand>
{
    public override ValidationResult Validate(ValidationContext<CreateLancamentoCommand> context)
    {
        RuleFor(x => x.TipoLancamentoId)
            .NotEmpty()
            .WithMessage("Tipo lançamento é obrigatória");
        
        RuleFor(x => x.Descricao)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Descrição é obrigatória");
        
        return base.Validate(context);
    }
}