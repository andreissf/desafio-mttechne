using AutoMapper;
using Lancamentos.Domain;
using Lancamentos.Infrastructure.Repository;
using MediatR;

namespace Lancamentos.Application.UseCases.Get;

public class GetLancamentoHandler :IRequestHandler<GetLancamentoCommand, Lancamento>
{
    private readonly ILancamentoRepository _lancamentoRepository;
    private readonly IMapper _mapper;

    public GetLancamentoHandler(ILancamentoRepository lancamentoRepository, IMapper mapper)
    {
        _lancamentoRepository = lancamentoRepository;
        _mapper = mapper;
    }
    
    public Task<Lancamento> Handle(GetLancamentoCommand request, CancellationToken cancellationToken)
    {
        var lancamento = _lancamentoRepository.GetById(request.LancamentoId);
        return Task.FromResult(_mapper.Map<Lancamento>(lancamento));
    }
}