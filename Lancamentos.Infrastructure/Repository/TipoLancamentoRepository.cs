using AutoMapper;
using Core.Data;
using Lancamentos.Domain;

namespace Lancamentos.Infrastructure.Repository;

public class TipoLancamentoRepository : ITipoLancamentoRepository
{
    private readonly LancamentosDbContext _lancamentosDbContext;
    private readonly IMapper _mapper;

    public TipoLancamentoRepository(LancamentosDbContext lancamentosDbContext, IMapper mapper)
    {
        UnitOfWork = lancamentosDbContext;
        _lancamentosDbContext = lancamentosDbContext;
        _mapper = mapper;
    }
    
    public IUnitOfWork UnitOfWork { get; }
    
    public IEnumerable<TipoLancamento> Get()
    {
        var lancamentos = _mapper.Map<IEnumerable<TipoLancamento>>(
            _lancamentosDbContext.TipoLancamentos.ToList());
        return lancamentos;
    }

    public TipoLancamento GetById(Guid id)
    {
        var lancamento = _mapper.Map<TipoLancamento>(
            _lancamentosDbContext.TipoLancamentos.Where(x => x.Id == id).FirstOrDefault());
        return lancamento;
    }
    
    public void Add(TipoLancamento tipoLancamento)
    {
        var entity = _mapper.Map<Entities.TipoLancamento>(tipoLancamento);
        _lancamentosDbContext.TipoLancamentos.Add(entity);
    }

    public void Update(TipoLancamento tipoLancamento)
    {
        var entity = _mapper.Map<Entities.TipoLancamento>(tipoLancamento);
        _lancamentosDbContext.TipoLancamentos.Update(entity);
    }

    public void Delete(TipoLancamento tipoLancamento)
    {
        var entity = _mapper.Map<Entities.TipoLancamento>(tipoLancamento);
        _lancamentosDbContext.TipoLancamentos.Remove(entity);
    }
}