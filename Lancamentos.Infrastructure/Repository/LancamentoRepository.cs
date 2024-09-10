using AutoMapper;
using Core.Data;
using Lancamentos.Domain;

namespace Lancamentos.Infrastructure.Repository;

public class LancamentoRepository : ILancamentoRepository
{
    private readonly LancamentosDbContext _lancamentosDbContext;
    private readonly IMapper _mapper;

    public LancamentoRepository(LancamentosDbContext lancamentosDbContext, IMapper mapper)
    {
        UnitOfWork = lancamentosDbContext;
        _lancamentosDbContext = lancamentosDbContext;
        _mapper = mapper;
    }

    public IUnitOfWork UnitOfWork { get; }
    
    public IEnumerable<Lancamento> Get()
    {
        var lancamentos = _mapper.Map<IEnumerable<Lancamento>>(_lancamentosDbContext.Lancamentos.ToList());
        return lancamentos;
    }

    public Lancamento GetById(Guid id)
    {
        var lancamento =
            _mapper.Map<Lancamento>(
                _lancamentosDbContext.Lancamentos.Where(x => x.Id == id).FirstOrDefault());
        return lancamento;
    }

    public void Add(Lancamento lancamento)
    {
        var entity = _mapper.Map<Entities.Lancamento>(lancamento);
        _lancamentosDbContext.Lancamentos.Add(entity);
    }

    public void Update(Lancamento lancamento)
    {
        var entity = _mapper.Map<Entities.Lancamento>(lancamento);
        _lancamentosDbContext.Lancamentos.Update(entity);
    }

    public void Delete(Lancamento lancamento)
    {
        var entity = _mapper.Map<Entities.Lancamento>(lancamento);
        _lancamentosDbContext.Lancamentos.Remove(entity);
    }
}