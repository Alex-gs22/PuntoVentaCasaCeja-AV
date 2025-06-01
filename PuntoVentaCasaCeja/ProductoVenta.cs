using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoVentaCasaCeja
{
    public class ProductoVenta
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int cantidad { get; set; }
        public double precio_venta { get; set; }
        // NUEVOS CAMPOS PARA PRECIO ESPECIAL
        public double precio_original { get; set; }  // Para guardar el precio original (menudeo)
        public bool es_precio_especial { get; set; } // Indica si tiene precio especial aplicado
        public double descuento_unitario { get; set; } // Descuento por unidad aplicado
    }
}
