using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using ElimperioAPI.Models;
using ElimperioAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace ElimperioAPI.Services
{
    public class ProductoService
    {
        private readonly IMongoCollection<Producto> _coleccionProducto;

        public ProductoService(IOptions<ImperioDBsettings> configuracionBD) 
        {
            var clienteMongo = new MongoClient(configuracionBD.Value.CadenaConexion);
            var BaseDatos = clienteMongo.GetDatabase(configuracionBD.Value.NombreBaseDatos);
            _coleccionProducto= BaseDatos.GetCollection<Producto>("Productos");
        }

        public async Task<List<Producto>> ObtenerAsync()=> await _coleccionProducto.Find(_ =>true).ToListAsync();
        public async Task<Producto>ObtenerAsync(string id)=> await _coleccionProducto.Find(x => x.Id ==id).FirstOrDefaultAsync();

        public async Task CrearAsync(Producto nuevoProducto)
        {
            await _coleccionProducto.InsertOneAsync(nuevoProducto);
        }

        public async Task ActualizarAsync(Producto ProductoUpdate)
        {
            var filter = Builders<Producto>.Filter.Eq(x=> x.Id, ProductoUpdate.Id);
            await _coleccionProducto.ReplaceOneAsync(filter, ProductoUpdate);
        }

        public async Task EliminarAsync(string id)
        {
            var filter = Builders<Producto>
                .Filter
                .Eq(x => x.Id, id);
            await _coleccionProducto.DeleteOneAsync(filter);
        }
    }
}
