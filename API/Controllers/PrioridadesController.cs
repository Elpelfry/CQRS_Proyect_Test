using API.Application.Commands.PrioridadesCommands;
using API.Application.Dtos;
using API.Application.Queries.PrioridadesQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrioridadesController(IMediator _mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<PrioridadesDto>> GetById(int id)
    {
        var priority = await _mediator.Send(new GetPrioridadByIdQuery(id));
        if (priority == null)
        {
            return NotFound();
        }

        return Ok(priority);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PrioridadesDto>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllPrioridadQuery()));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CrearPrioridadCommand command)
    {
        var prioridad = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = prioridad.Id }, prioridad);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ActualizarPrioridadCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new BorrarPrioridadCommand(id));
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
