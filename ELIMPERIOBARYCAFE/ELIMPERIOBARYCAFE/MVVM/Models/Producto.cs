﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIMPERIOBARYCAFE.MVVM.Models
{
    class Producto
    {
        public string? Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Precio { get; set; } = null!;
        public string Stock { get; set; } = null!;
        public string Categoria {  get; set; } = null!;
    }
}
