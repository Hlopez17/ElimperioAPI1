using ElimperioAPI.Data;
using ElimperioAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ElimperioAPI.Services
{
    public class CajaService
    {
        private readonly IMongoCollection<Mesa> _mesas;
        private readonly IMongoCollection<Caja> _caja;

        //private static int _Numeropedido = 1;
        private static string _estado = "Pendiente";

        public CajaService(IOptions<ImperioDBsettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.CadenaConexion);
            var database = client.GetDatabase(mongoSettings.Value.NombreBaseDatos);
            _mesas = database.GetCollection<Mesa>("Mesas");
            _caja = database.GetCollection<Caja>("Caja");

        }

        public async Task<Mesa?> ObtenerMesaPendienteAsync(int numeroMesa)
        {
            return await _mesas.Find(m => m.NumeroMesa == numeroMesa && m.Estado == "Pendiente").FirstOrDefaultAsync();
        }

        public async Task<List<Mesa>> ObtenerMesasPendientesAsync() =>
     await _mesas.Find(mesa => mesa.Estado == "Pendiente").ToListAsync();


        public async Task<bool> RegistrarPagoAsync(Caja nuevaCaja)
        {
            // Buscar la mesa asociada al número
            var mesa = await ObtenerMesaPendienteAsync(nuevaCaja.NumeroMesa);
            if (mesa == null) return false; // Mesa no encontrada o ya cancelada

            // Insertar en la colección Caja
            await _caja.InsertOneAsync(nuevaCaja);

            // Actualizar estado de la mesa a "Cancelado"
            var filtro = Builders<Mesa>.Filter.Eq(m => m.Id, mesa.Id);
            var actualizacion = Builders<Mesa>.Update.Set(m => m.Estado, "Cancelado");
            await _mesas.UpdateOneAsync(filtro, actualizacion);

            return true;
        }
    }
}
