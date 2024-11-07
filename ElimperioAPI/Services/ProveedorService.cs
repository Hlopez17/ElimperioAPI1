using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using ElimperioAPI.Models;
using ElimperioAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace ElimperioAPI.Services
{
    public class ProveedorService
    {
        private readonly IMongoCollection<Proveedor> _coleccionProveedor; //Se crea la referencia a la Colección a utilizarse

        public ProveedorService(IOptions<ImperioDBsettings> configuracionBD)
        {
            var clienteMongo = new MongoClient(configuracionBD.Value.CadenaConexion);
            var BaseDatos = clienteMongo.GetDatabase(configuracionBD.Value.NombreBaseDatos);
            _coleccionProveedor = BaseDatos.GetCollection<Proveedor>("Proveedores");
        }

        public async Task<List<Proveedor>> ObtenerAsync() => await _coleccionProveedor.Find(_ => true).ToListAsync();
        public async Task<Proveedor> ObtenerAsync(string id) => await _coleccionProveedor.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Proveedor>> Get()
          => await _coleccionProveedor.FindAsync(
              new BsonDocument()).Result.ToListAsync();

        public async Task<Proveedor> GetById(string id)
            => await _coleccionProveedor.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

        public async Task InsertarProvAsync(Proveedor Suppliers)
        {
            await _coleccionProveedor.InsertOneAsync(Suppliers);
        }



        public async Task ActualizarAsync(Proveedor ReservaUpdate)
        {
            var filter = Builders<Proveedor>.Filter.Eq(x => x.Id, ReservaUpdate.Id);
            await _coleccionProveedor.ReplaceOneAsync(filter, ReservaUpdate);
        }

        public async Task EliminarAsync(string id)
        {
            var filter = Builders<Proveedor>
                .Filter
                .Eq(x => x.Id, id);
            await _coleccionProveedor.DeleteOneAsync(filter);
        }
    }
}
