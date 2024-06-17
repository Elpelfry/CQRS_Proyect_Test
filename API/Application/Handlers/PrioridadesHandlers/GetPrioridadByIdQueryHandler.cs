using API.Application.Dtos;
using API.Application.Queries.PrioridadesQueries;
using API.Infratructure.Data;
using MediatR;
using MongoDB.Driver;

namespace API.Application.Handlers.PrioridadesHandlers;

public class GetPrioridadByIdQueryHandler(MongoDbContext _mongoContext) : IRequestHandler<GetPrioridadByIdQuery, PrioridadesDto>
{
    public async Task<PrioridadesDto> Handle(GetPrioridadByIdQuery request, CancellationToken cancellationToken)
    {
        var priority = await _mongoContext.Priorities.Find(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        if (priority == null)
        {
            return null!;
        }

        return new PrioridadesDto
        {
            Id = priority.Id,
            Descripcion = priority.Descripcion,
            DiasCompromiso = priority.DiasCompromiso
        };
    }
}
