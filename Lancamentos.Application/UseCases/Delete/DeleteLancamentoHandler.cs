using FluentValidation.Results;
using Lancamentos.Infrastructure.Repository;
using MediatR;

namespace Lancamentos.Application.UseCases.Delete;

public class DeleteLancamentoHandler : IRequestHandler<DeleteLancamentoCommand, ValidationResult>
{
    private readonly ILancamentoRepository _lancamentoRepository;
    public DeleteLancamentoHandler(ILancamentoRepository lancamentoRepository)
    {
        _lancamentoRepository = lancamentoRepository;
    }
    public async Task<ValidationResult> Handle(DeleteLancamentoCommand request, CancellationToken cancellationToken)
    {
        if(!request.EhValido())
        {
            return request.ValidationResult;
        }
        
        var lancamento = _lancamentoRepository.GetById(request.LancamentoId);

        _lancamentoRepository.Delete(lancamento);
        
        if(!await _lancamentoRepository.UnitOfWork.Commit())
        {
            request.ValidationResult.Errors.Add(new ValidationFailure("Lancamento", "Erro ao deletar o lancamento"));
        }
        
        return request.ValidationResult;
    }
    
    
}