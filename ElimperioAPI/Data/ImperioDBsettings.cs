namespace ElimperioAPI.Data
{
    public class ImperioDBsettings
    {
        public string CadenaConexion { get; set; } = null!;
        public string NombreBaseDatos { get; set; } = null!;
        public string ColeccionImperio { get; set; } = null!;

        public string ColeccionUser { get; set; } = null!;
        public string ColeccionReserva {  get; set; } = null!;
        public string ColeccionProveedor {  get; set; } = null!;
        public string ColeccionMesas {  get; set; } = null!;
        public string ColeccionCaja {  get; set; } = null!;
    }
}
