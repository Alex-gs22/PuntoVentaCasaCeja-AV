﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoVentaCasaCeja
{
    public class Categoria
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int activo { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int isdescuento { get; set; }
        public double descuento { get; set; }
    }
}
