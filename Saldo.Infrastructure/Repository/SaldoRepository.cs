using AutoMapper;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using Saldo.Infrastructure;
using Saldo.Domain;

namespace Relatorio.Infrastructure.Repository;

public class SaldoRepository : ISaldoRepository
{
    private readonly SaldoDbContext _dbContext;
    private readonly IMapper _mapper;

    public SaldoRepository(SaldoDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        UnitOfWork = dbContext;
        _mapper = mapper;
    }
    
    public IUnitOfWork UnitOfWork { get; }
    
    public async Task<Saldo.Domain.Saldo> GetByDate(DateTime data)
    {
        var saldo = await _dbContext.Saldos.FirstOrDefaultAsync(x => x.Data == data);
        var map = _mapper.Map<Saldo.Domain.Saldo>(saldo);
        return map;
    }

    public decimal GetUltimoSaldo()
    {
        return _dbContext.Saldos.OrderByDescending(x => x.Data).FirstOrDefault()?.Valor ?? 0;
    }

    public IEnumerable<Saldo.Domain.Saldo> Get()
    {
        return _mapper.Map<IEnumerable<Saldo.Domain.Saldo>>(
            _dbContext.Saldos.ToList());
    }

    public Saldo.Domain.Saldo GetById(Guid id)
    {
        return _mapper.Map<Saldo.Domain.Saldo>(
            _dbContext.Saldos.Where(x => x.Id == id).SingleOrDefault());
    }

    public void Add(Saldo.Domain.Saldo saldo)
    {
        _dbContext.Saldos.Add(_mapper.Map<Saldo.Infrastructure.Entities.Saldo>(saldo));
    }

    public void Update(Saldo.Domain.Saldo saldo)
    {
        _dbContext.Saldos.Update(_mapper.Map<Saldo.Infrastructure.Entities.Saldo>(saldo));
    }

    public void Delete(Saldo.Domain.Saldo saldo)
    {
        _dbContext.Saldos.Remove(_mapper.Map<Saldo.Infrastructure.Entities.Saldo>(saldo));
    }
}