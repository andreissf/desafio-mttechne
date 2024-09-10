using FluentValidation.Results;
using MediatR;

namespace Lancamentos.Application.UseCases.Delete;

public class DeleteLancamentoCommand : IRequest<ValidationResult>
{
    public ValidationResult ValidationResult { get; set; }
    public Guid LancamentoId { get; set; }

    public DeleteLancamentoCommand(Guid lancamentoId)
    {
        LancamentoId = lancamentoId;
    }
    
    public bool EhValido()
    {
        ValidationResult = new DeleteLancamentoValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}