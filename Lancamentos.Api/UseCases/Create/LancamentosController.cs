using System.Net.Sockets;
using Lancamentos.Application.UseCases.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Lancamentos.Application.Service;

namespace Lancamentos.Api.UseCases.Create;

public class LancamentosController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ISaldoService _saldoService;

    public LancamentosController(IMediator mediator, IMapper mapper, ISaldoService saldoService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _saldoService = saldoService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("api/lancamentos")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateLancamentoRequest input)
    {
        try
        {
            await _saldoService.GetHealth();
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
        catch (SocketException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
        
        var command = _mapper.Map<CreateLancamentoCommand>(input);
        var resultado = await _mediator.Send(command);
        
        if(!resultado.IsValid)
            return BadRequest(resultado.Errors);
        
        return Created("api/lancamentos", new { criado = true });
    }
}