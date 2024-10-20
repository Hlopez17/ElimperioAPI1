using ElimperioAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ElimperioAPI.Data;
using MongoDB.Bson;

namespace ElimperioAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _coleccionUsuarios;

        public UserService(IOptions<ImperioDBsettings> ConfiguracionBD)
        {
            var clientemongo = new MongoClient(ConfiguracionBD.Value.CadenaConexion);
            var BaseDatos = clientemongo.GetDatabase(ConfiguracionBD.Value.NombreBaseDatos);
            _coleccionUsuarios = BaseDatos.GetCollection<User>("Users");

        }

        public async Task<User?> ObtenerUsuarioAsync(string username)
           => await _coleccionUsuarios.Find(u => u.Username == username).FirstOrDefaultAsync();

        public async Task CrearUsuarioAsync(User nuevoUsuario)

            => await _coleccionUsuarios.InsertOneAsync(nuevoUsuario);

        public async Task<List<User>> Get()
           => await _coleccionUsuarios.FindAsync(
               new BsonDocument()).Result.ToListAsync();
    }
}
