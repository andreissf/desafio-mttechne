using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saldo.Application.UseCases.Get;

namespace Saldo.Api.UseCases.Get;

public class ConsolidadoDiarioController : Controller
{
    private readonly IMediator _mediator;
    
    public ConsolidadoDiarioController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("api/consolidado-diario")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Index()
    {
        var saldo = await _mediator.Send(new GetConsolidadoDiarioCommand());
        
        return Ok(saldo);
    }
}