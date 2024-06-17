using API.Application.Commands.PrioridadesCommands;
using API.Domain.Entities;
using API.Infratructure.Data;
using MediatR;
using MongoDB.Driver;

namespace API.Application.Handlers.PrioridadesHandlers;

public class BorrarPrioridadCommandHandler(SqlDbContext _sqlContext, MongoDbContext _mongoContext) : IRequestHandler<BorrarPrioridadCommand, bool>
{
    public async Task<bool> Handle(BorrarPrioridadCommand request, CancellationToken cancellationToken)
    {
        var prioridad = await _sqlContext.Prioridades.FindAsync(request.Id);

        if (prioridad == null)
        {
            return false;
        }

        _sqlContext.Prioridades.Remove(prioridad);
        var result = await _sqlContext.SaveChangesAsync(cancellationToken) > 0;

        if (result)
        {
            // Eliminar de MongoDB
            var filter = Builders<Prioridades>.Filter.Eq(p => p.Id, prioridad.Id);
            await _mongoContext.Priorities.DeleteOneAsync(filter, cancellationToken);
        }

        return result;
    }
}
