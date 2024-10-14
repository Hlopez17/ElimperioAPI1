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
            _coleccionreservas = BaseDatos.GetCollection<Reservas>(configuracionBD.Value.ColeccionImperio);
        }

        public async Task<List<Reservas>> Get()
          => await _coleccionreservas.FindAsync(
              new BsonDocument()).Result.ToListAsync();

        public async Task<Reservas> GetById(string id)
            => await _coleccionreservas.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

        public async Task Create(Reservas product)
            => await _coleccionreservas.InsertOneAsync(product);

        public async Task Update(Reservas product)
        {
            var filter = Builders<Reservas>
                .Filter
                .Eq(x => x.Id, product.Id);

            await _coleccionreservas.ReplaceOneAsync(filter, product);
        }

        //public async Task Delete(string id)
        //{
        //    var filter = Builders<Reservas>
        //        .Filter
        //        .Eq(x => x.Id, new ObjectId(id));

        //    await _coleccionreservas.DeleteOneAsync(filter);
        //}
    }
}
