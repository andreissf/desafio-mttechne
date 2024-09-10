using Lancamentos.Application.UseCases.Get;
using Lancamentos.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lancamentos.Api.UseCases.Get;

public class LancamentosController : Controller
{
    private readonly IMediator _mediator;

    public LancamentosController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("api/lancamentos")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Lancamento>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetLancamentosCommand());
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("api/lancamentos/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Lancamento))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetLancamentoCommand(){ LancamentoId = id});
        return Ok(response);
    }
}