using AutoMapper.Configuration.Annotations;
using FluentValidation.Results;
using MediatR;

namespace Lancamentos.Application.UseCases.Create;

public class CreateLancamentoCommand : IRequest<ValidationResult>
{
    public ValidationResult ValidationResult { get; set; }
    public Guid TipoLancamentoId { get; set; }
    public string Descricao { get; set; } = default!;
    public decimal Valor { get; set; }

    public CreateLancamentoCommand(Guid tipoLancamentoId, string descricao, decimal valor)
    {
        TipoLancamentoId = tipoLancamentoId;
        Descricao = descricao;
        Valor = valor;
    }
    
    public bool EhValido()
    {
        ValidationResult = new CreateLancamentoValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}