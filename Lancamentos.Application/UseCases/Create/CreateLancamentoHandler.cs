using AutoMapper;
using Core.Messages.Integration;
using FluentValidation.Results;
using Lancamentos.Domain;
using Lancamentos.Infrastructure.Repository;
using MediatR;
using MessageBus;

namespace Lancamentos.Application.UseCases.Create;

public class CreateLancamentoHandler : IRequestHandler<CreateLancamentoCommand, ValidationResult>
{
    private readonly ILancamentoRepository _lancamentoRepository;
    private readonly ITipoLancamentoRepository _tipoLancamentoRepository;
    private readonly IMessageBus _bus;
    private readonly IMapper _mapper;
    
    public CreateLancamentoHandler(ILancamentoRepository lancamentoRepository,
        ITipoLancamentoRepository tipoLancamentoRepository, 
        IMessageBus bus,
        IMapper mapper)
    {
        _lancamentoRepository = lancamentoRepository;
        _tipoLancamentoRepository = tipoLancamentoRepository;
        _mapper = mapper;
        _bus = bus;
    }
    
    public async Task<ValidationResult> Handle(CreateLancamentoCommand request, CancellationToken cancellationToken)
    {
        if(!request.EhValido()) return request.ValidationResult;
        
        _lancamentoRepository.Add(_mapper.Map<Lancamento>(request));
       
        if(!await _lancamentoRepository.UnitOfWork.Commit())
        {
            request.ValidationResult.Errors.Add(new ValidationFailure("", "Erro ao persistir dados."));
            return request.ValidationResult;
        }

        var tipoLancamento = _tipoLancamentoRepository.GetById(request.TipoLancamentoId);
        if(tipoLancamento is null)
        {
            request.ValidationResult.Errors.Add(new ValidationFailure("", "Tipo de lançamento não encontrado."));
            return request.ValidationResult;
        }
        
        if (!tipoLancamento.EhDebito() && request.Valor < 0)
        {
            request.ValidationResult.Errors.Add(new ValidationFailure("", "Valor do lançamento para crédito deve ser maior que zero."));
            return request.ValidationResult;
        }
        
        if(tipoLancamento.EhDebito() && request.Valor > 0)
        {
            request.Valor = request.Valor * -1;
        }

        var datetimeNow = DateTime.Now;
        var data = new DateTime(datetimeNow.Year, datetimeNow.Month, datetimeNow.Day);
        await _bus.PublishAsync(new LancamentoRealizadoIntegrationEvent(request.TipoLancamentoId, request.Valor, data));
        
        return request.ValidationResult;
    }
}