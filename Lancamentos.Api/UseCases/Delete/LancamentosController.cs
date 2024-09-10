using Lancamentos.Application.UseCases.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lancamentos.Api.UseCases.Delete;

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
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("api/lancamentos/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var deleteLancamentoValidator = await _mediator.Send(new DeleteLancamentoCommand(id));

        if (!deleteLancamentoValidator.IsValid)
        {
            return BadRequest(deleteLancamentoValidator.Errors);
        }
        
        return Ok();
    }
}