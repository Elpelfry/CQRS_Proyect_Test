using API.Application.Dtos;
using MediatR;

namespace API.Application.Commands.PrioridadesCommands;

public record ActualizarPrioridadCommand(int Id, string? Descripcion, int DiasCompromiso) : IRequest<PrioridadesDto>;
