using MongoDB.Driver;
using MongoDB.Bson;
using ElimperioAPI.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElimperioAPI.Data;

namespace ElimperioAPI.Services
{
    public class MesaService
    {
        private readonly IMongoCollection<Mesa> _mesas;
        private readonly IMongoCollection<Pedido> _pedidos;

        //private static int _Numeropedido = 1;
        private static string _estado = "Pendiente";

        public MesaService(IOptions<ImperioDBsettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.CadenaConexion);
            var database = client.GetDatabase(mongoSettings.Value.NombreBaseDatos);
            _mesas = database.GetCollection<Mesa>("Mesas");
         
        }


        public async Task<List<Mesa>> GetMesasAsync() =>
            await _mesas.Find(mesa => true).ToListAsync();

        public async Task<Mesa> GetMesaAsync(int numeroMesa) =>
            await _mesas.Find<Mesa>(mesa => mesa.NumeroMesa == numeroMesa).FirstOrDefaultAsync();

        public async Task<Mesa> CreateMesaAsync(Mesa nuevaMesa)
        {
            nuevaMesa.Estado = _estado;
            await _mesas.InsertOneAsync(nuevaMesa);
            return nuevaMesa;
        }

        public async Task UpdateMesaAsync(string id, Mesa mesaActualizada) =>
            await _mesas.ReplaceOneAsync(mesa => mesa.Id == id, mesaActualizada);

        public async Task DeleteMesaAsync(string id) =>
            await _mesas.DeleteOneAsync(mesa => mesa.Id == id);
        public async Task<Mesa> GetMesaByIdAsync(string id) =>
        await _mesas.Find<Mesa>(mesa => mesa.Id == id).FirstOrDefaultAsync();

        


    }
}
