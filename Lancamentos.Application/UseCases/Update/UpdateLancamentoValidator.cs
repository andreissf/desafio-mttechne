using System.Data;
using FluentValidation.Results;

namespace Lancamentos.Application.UseCases.Update;

using FluentValidation;

public class UpdateLancamentoValidator : AbstractValidator<UpdateLancamentoCommand>
{
    public override ValidationResult Validate(ValidationContext<UpdateLancamentoCommand> context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id é obrigatória");
        
        RuleFor(x => x.Descricao)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Descrição é obrigatória");
        
        RuleFor(x => x.Valor)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Data é obrigatória");
        
        return base.Validate(context);
    }
}