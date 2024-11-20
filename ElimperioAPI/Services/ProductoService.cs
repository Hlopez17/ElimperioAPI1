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

        //Mostrar los productos en lista
        public async Task<List<Producto>> ObtenerAsync()=> await _coleccionProducto.Find(_ =>true).ToListAsync();

        //Método de búsqueda por id
        public async Task<Producto>ObtenerAsync(string id)=> await _coleccionProducto.Find(x => x.Id ==id).FirstOrDefaultAsync();
        //Tarea Async para crear producto
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

        public async Task<List<Producto>> BuscarPorNombreAsync(string nombre)
        {
            // Usar un filtro de búsqueda insensible a mayúsculas y minúsculas para coincidencias parciales
            var filter = Builders<Producto>.Filter.Regex("Descripcion", new MongoDB.Bson.BsonRegularExpression(nombre, "i"));
            return await _coleccionProducto.Find(filter).ToListAsync();
        }
    }
}
