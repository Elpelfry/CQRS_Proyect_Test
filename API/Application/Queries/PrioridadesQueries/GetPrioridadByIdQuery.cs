using API.Application.Dtos;
using MediatR;

namespace API.Application.Queries.PrioridadesQueries;

public record GetPrioridadByIdQuery(int Id) : IRequest<PrioridadesDto>;
