using API.Application.Commands.PrioridadesCommands;
using API.Application.Dtos;
using API.Domain.Entities;
using API.Infratructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace API.Application.Handlers.PrioridadesHandlers;

public class ActualizarPrioridadCommandHandler(SqlDbContext _sqlContext, MongoDbContext _mongoContext) : IRequestHandler<ActualizarPrioridadCommand, PrioridadesDto>
{
    public async Task<PrioridadesDto> Handle(ActualizarPrioridadCommand request, CancellationToken cancellationToken)
    {
        var prioridad = new Prioridades
        {
            Id = request.Id,
            Descripcion = request.Descripcion,
            DiasCompromiso = request.DiasCompromiso
        };

        _sqlContext.Entry(prioridad).State = EntityState.Modified;
        await _sqlContext.SaveChangesAsync(cancellationToken);

        // Sincronizar con MongoDB
        var filter = Builders<Prioridades>.Filter.Eq(p => p.Id, prioridad.Id);
        var update = Builders<Prioridades>.Update
            .Set(p => p.Descripcion, prioridad.Descripcion)
            .Set(p => p.DiasCompromiso, prioridad.DiasCompromiso);
        await _mongoContext.Priorities.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        return new PrioridadesDto
        {
            Id = prioridad.Id,
            Descripcion = prioridad.Descripcion,
            DiasCompromiso = prioridad.DiasCompromiso
        };
    }
}

