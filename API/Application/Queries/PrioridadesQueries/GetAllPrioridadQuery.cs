using API.Application.Dtos;
using MediatR;

namespace API.Application.Queries.PrioridadesQueries;

public record GetAllPrioridadQuery : IRequest<IEnumerable<PrioridadesDto>>;
