using System;

public class Corte
{
    public int id { get; set; }
    public decimal fondo_apertura { get; set; }
    public decimal total_efectivo { get; set; }
    public string folio_corte { get; set; }
    public decimal total_tarjetas_debito { get; set; }
    public decimal total_tarjetas_credito { get; set; }
    public decimal total_cheques { get; set; }
    public decimal total_transferencias { get; set; }
    public decimal efectivo_apartdos { get; set; }
    public decimal efectivo_creditos { get; set; }
    public decimal gastos { get; set; }
    public decimal sobrante { get; set; }
    public DateTime fecha_apertura_caja { get; set; }
    public DateTime fecha_corte_caja { get; set; }
    public int sucursal_id { get; set; }
    public int usuario_id { get; set; }
}
