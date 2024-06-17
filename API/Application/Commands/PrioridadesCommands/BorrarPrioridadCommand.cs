using MediatR;

namespace API.Application.Commands.PrioridadesCommands;

public record BorrarPrioridadCommand(int Id) : IRequest<bool>;