using AutoMapper;
using Lancamentos.Domain;
using Lancamentos.Infrastructure.Repository;
using MediatR;

namespace Lancamentos.Application.UseCases.Get;

public class GetLancamentosHandler : IRequestHandler<GetLancamentosCommand, IEnumerable<Lancamento>>
{
    private readonly ILancamentoRepository _lancamentoRepository;
    private readonly IMapper _mapper;

    public GetLancamentosHandler(ILancamentoRepository lancamentoRepository, IMapper mapper)
    {
        _lancamentoRepository = lancamentoRepository;
        _mapper = mapper;
    }
    
    public Task<IEnumerable<Lancamento>> Handle(GetLancamentosCommand request, CancellationToken cancellationToken)
    {
        var lancamentos = _lancamentoRepository.Get();
        return Task.FromResult(_mapper.Map<IEnumerable<Lancamento>>(lancamentos));
    }
}