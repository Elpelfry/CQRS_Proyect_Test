using API.Application.Dtos;
using MediatR;

namespace API.Application.Commands.PrioridadesCommands;

public record CrearPrioridadCommand(string? Descripcion, int DiasCompromiso) : IRequest<PrioridadesDto>;

