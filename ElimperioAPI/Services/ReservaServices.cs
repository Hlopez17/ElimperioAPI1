using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using ElimperioAPI.Models;
using ElimperioAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace ElimperioAPI.Services
{
    public class ReservaServices
    {
        private readonly IMongoCollection<Reservas> _coleccionreservas;

        public ReservaServices(IOptions<ImperioDBsettings> configuracionBD)

        {
            var clienteMongo = new MongoClient(configuracionBD.Value.CadenaConexion);
            var BaseDatos = clienteMongo.GetDatabase(configuracionBD.Value.NombreBaseDatos);
            _coleccionreservas = BaseDatos.GetCollection<Reservas>("Reservas");
        }


        public async Task<List<Reservas>> ObtenerAsync() => await _coleccionreservas.Find(_ => true).ToListAsync();
        public async Task<Reservas> ObtenerAsync(string id) => await _coleccionreservas.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Reservas>> Get()
          => await _coleccionreservas.FindAsync(
              new BsonDocument()).Result.ToListAsync();

        public async Task<Reservas> GetById(string id)
            => await _coleccionreservas.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

        public async Task InsertarReservaAsync(Reservas reserva)
        {
            await _coleccionreservas.InsertOneAsync(reserva);
        }

        public async Task ActualizarAsync(Reservas ReservaUpdate)
        {
            var filter = Builders<Reservas>.Filter.Eq(x => x.Id, ReservaUpdate.Id);
            await _coleccionreservas.ReplaceOneAsync(filter, ReservaUpdate);
        }

        public async Task EliminarAsync(string id)
        {
            var filter = Builders<Reservas>
                .Filter
                .Eq(x => x.Id, id);
            await _coleccionreservas.DeleteOneAsync(filter);
        }
    }
}
