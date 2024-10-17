using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIMPERIOBARYCAFE.MVVM.Models
{
    internal class User
    {
        public string? Id { get; set; }
        public string Nombre_Completo { get; set; } = null!;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Formato de email no válido.")]
        public string Email { get; set; } = null!;
    }
}
