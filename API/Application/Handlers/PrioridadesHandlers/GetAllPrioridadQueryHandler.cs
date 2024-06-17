using API.Application.Dtos;
using API.Application.Queries.PrioridadesQueries;
using API.Infratructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace API.Application.Handlers.PrioridadesHandlers;

public class GetAllPrioridadQueryHandler(MongoDbContext _mongoContext) : IRequestHandler<GetAllPrioridadQuery, IEnumerable<PrioridadesDto>>
{
    public async Task<IEnumerable<PrioridadesDto>> Handle(GetAllPrioridadQuery request, CancellationToken cancellationToken)
    {
        var priorities = await _mongoContext.Priorities.Find(_ => true).ToListAsync(cancellationToken);
        return priorities
            .Select(p => new PrioridadesDto
            {
                Id = p.Id,
                Descripcion = p.Descripcion,
                DiasCompromiso = p.DiasCompromiso

            }).ToList();
    }
}