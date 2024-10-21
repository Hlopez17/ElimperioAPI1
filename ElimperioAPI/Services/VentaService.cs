using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using ElimperioAPI.Models;
using ElimperioAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace ElimperioAPI.Services
{
    public class VentaService
    {
        private readonly IMongoCollection<Venta> _coleccionventa;

        public VentaService(IOptions<ImperioDBsettings> ConfiguracionBD)
        {
            var clienteMongo = new MongoClient(ConfiguracionBD.Value.CadenaConexion);
            var BaseDatos = clienteMongo.GetDatabase(ConfiguracionBD.Value.NombreBaseDatos);
            _coleccionventa = BaseDatos.GetCollection<Venta>("Ventas");
        }

        public async Task<List<Venta>> ObtenerAsync()=> await _coleccionventa.Find(_ => true).ToListAsync();


    }
}
