﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoVentaCasaCeja
{
    public class AbonoCredito
    {
        public int id { get; set; }
        public string folio { get; set; }
        public string metodo_pago { get; set; }
        public double total_abonado { get; set; }
        public string fecha { get; set; }
        public string folio_credito { get; set; }
        public int credito_id { get; set; }
        public string folio_corte { get; set; }
        public int usuario_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
