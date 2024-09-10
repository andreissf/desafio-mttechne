using AutoMapper;
using Lancamentos.Domain;
using Lancamentos.Infrastructure.Repository;
using MediatR;

namespace Lancamentos.Application.UseCases.Update;

public class UpdateLancamentoHandler : IRequestHandler<UpdateLancamentoCommand, Lancamento>
{
    private readonly ILancamentoRepository _lancamentoRepository;
    private readonly IMapper _mapper;
    
    public UpdateLancamentoHandler(ILancamentoRepository lancamentoRepository, IMapper mapper)
    {
        _lancamentoRepository = lancamentoRepository;
        _mapper = mapper;
    }
    
    public async Task<Lancamento> Handle(UpdateLancamentoCommand request, CancellationToken cancellationToken)
    {
        var lancamento = _lancamentoRepository.GetById(request.Id);
        if (lancamento == null)
        {
            return new Lancamento();
        }

        lancamento.Descricao = request.Descricao;
        lancamento.Valor = request.Valor;
        
        _lancamentoRepository.Update(lancamento);

        if (await _lancamentoRepository.UnitOfWork.Commit())
        {
            return _mapper.Map<Domain.Lancamento>(lancamento);
        };
        
        return new Lancamento();
    }
}