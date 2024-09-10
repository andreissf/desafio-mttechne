using AutoMapper;
using Lancamentos.Application.UseCases.Update;
using Lancamentos.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lancamentos.Api.UseCases.Update;

public class LancamentosController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public LancamentosController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("api/lancamentos/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Lancamento))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLancamentoRequest input)
    {
        var lancamento = _mapper.Map<UpdateLancamentoCommand>(input);
        lancamento.Id = id;
        
        var lancamentoAtualizado = await _mediator.Send(lancamento);
        
        return Ok(lancamentoAtualizado);
    }
}