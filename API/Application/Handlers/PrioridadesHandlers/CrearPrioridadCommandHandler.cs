using API.Application.Commands.PrioridadesCommands;
using API.Application.Dtos;
using API.Domain.Entities;
using API.Infratructure.Data;
using MediatR;

namespace API.Application.Handlers.PrioridadesHandlers;

public class CrearPrioridadCommandHandler(SqlDbContext _sqlContext, MongoDbContext _mongoContext) : IRequestHandler<CrearPrioridadCommand, PrioridadesDto>
{
    public async Task<PrioridadesDto> Handle(CrearPrioridadCommand request, CancellationToken cancellationToken)
    {
        var prioridad = new Prioridades
        {
            Descripcion = request.Descripcion,
            DiasCompromiso = request.DiasCompromiso
        };

        _sqlContext.Prioridades.Add(prioridad);
        await _sqlContext.SaveChangesAsync(cancellationToken);

        // Insertar en MongoDB
        var mongoPriority = new Prioridades { 
            Id = prioridad.Id, 
            Descripcion = request.Descripcion, 
            DiasCompromiso =request.DiasCompromiso 
        };
        await _mongoContext.Priorities.InsertOneAsync(mongoPriority, cancellationToken: cancellationToken);

        return new PrioridadesDto
        {
            Id = prioridad.Id,
            Descripcion = prioridad.Descripcion,
            DiasCompromiso = prioridad.DiasCompromiso
        };
    }
}
