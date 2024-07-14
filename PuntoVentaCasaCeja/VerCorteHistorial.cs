using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoVentaCasaCeja
{
    public partial class VerCorteHistorial : Form
    {
        Action<Usuario> setUser;
        Usuario usuario;
        WebDataManager webDM;
        LocaldataManager localDM;
        CurrentData data;
        Dictionary<string, string> corteData;
        public VerCorteHistorial(Dictionary<string, string> corteData, CurrentData data)
        {
            InitializeComponent();

            this.webDM = data.webDM;
            this.data = data;
            this.localDM = webDM.localDM;
            this.corteData = corteData;
            this.txtefeapa.Text = corteData["fecha_apertura_caja"];
            this.txtcheques.Text = corteData["total_cheques"];
            this.txtcredito.Text = corteData["total_tarjetas_credito"];
            this.txtefectivo.Text = corteData["total_efectivo"];
            this.txtdebito.Text = corteData["total_tarjetas_debito"];
            this.txtapertura.Text = corteData["fondo_apertura"];
            this.txttransferencias.Text = corteData["total_transferencias"];
            this.txtsobrante.Text = corteData["sobrante"];
            this.txtfolio.Text = corteData["folio_corte"];
            this.txtfechcorte.Text = corteData["fecha_corte_caja"];
            this.txttotapa.Text = corteData["efectivo_apartados"];
            this.txttotcred.Text = corteData["efectivo_creditos"];
            this.listagastos.DataSource = JsonConvert.DeserializeObject<Dictionary<string, double>>(corteData["gastos"]).ToList();
            this.listaingresos.DataSource = JsonConvert.DeserializeObject<Dictionary<string, double>>(corteData["ingresos"]).ToList();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void imprimirCorte(Dictionary<string, string> corte)
        {
            double efedir = double.Parse(corte["total_efectivo"]) - double.Parse(corte["efectivo_apartados"]) - double.Parse(corte["efectivo_creditos"]);
            double tgastos = 0;
            double tingresos = 0;

            Dictionary<string, double> gastos = JsonConvert.DeserializeObject<Dictionary<string, double>>(corte["gastos"]);
            foreach (var x in gastos)
            {
                tgastos += x.Value;
            }
            Dictionary<string, double> ingresos = JsonConvert.DeserializeObject<Dictionary<string, double>>(corte["ingresos"]);
            foreach (var x in ingresos)
            {
                tingresos += x.Value;
            }

            double totalCZ = double.Parse(corte["total_efectivo"]) + double.Parse(corte["total_tarjetas_debito"]) + double.Parse(corte["total_tarjetas_credito"]) + double.Parse(corte["total_cheques"]) + double.Parse(corte["total_transferencias"]) + double.Parse(corte["sobrante"]);
            string nc = localDM.getNombreUsuario(int.Parse(corte["usuario_id"]));
            CreaTicket Ticket1 = new CreaTicket();
            Ticket1.impresora = localDM.impresora;
            Ticket1.TextoCentro("CASA CEJA");
            Ticket1.TextoCentro(" ");
            Ticket1.TextoCentro("SUCURSAL: " + localDM.getSucursalname(int.Parse(corte["sucursal_id"])).ToUpper());
            Ticket1.TextoCentro(" ");

            Ticket1.TextoCentro("CZ FOLIO:  " + corte["folio_corte"]);
            Ticket1.TextoCentro(" ");
            Ticket1.LineasGuion(); // imprime una linea de guiones
            Ticket1.TextoExtremos("FECHA DE APERTURA:", corte["fecha_apertura_caja"]);
            Ticket1.TextoExtremos("FECHA DE CORTE:", corte["fecha_corte_caja"]);
            Ticket1.LineasGuion(); // imprime una linea de guiones
            Ticket1.TextoCentro(" ");

            Ticket1.TextoExtremos("FONDO DE APERTURA:", corte["fondo_apertura"]);
            Ticket1.TextoCentro(" ");

            Ticket1.LineasGuion();
            Ticket1.TextoExtremos("EFECTIVO DE CREDITOS:", corte["efectivo_creditos"]);
            Ticket1.TextoExtremos("EFECTIVO DE APARTADOS:", corte["efectivo_apartados"]);
            Ticket1.TextoExtremos("EFECTIVO DIRECTO: ", efedir.ToString("0.00"));
            Ticket1.LineasGuion();
            Ticket1.TextoCentro(" ");
            Ticket1.TextoExtremos("EFECTIVO TOTAL: ", corte["total_efectivo"]);
            Ticket1.TextoCentro(" ");
            Ticket1.LineasGuion();
            Ticket1.TextoExtremos("TOTAL T. DEBITO", corte["total_tarjetas_debito"]);
            Ticket1.TextoExtremos("TOTAL T. CREDITO", corte["total_tarjetas_credito"]);
            Ticket1.TextoExtremos("TOTAL CHEQUES", corte["total_cheques"]);
            Ticket1.TextoExtremos("TOTAL TRANSFERENCIAS", corte["total_transferencias"]);
            Ticket1.LineasGuion();
            Ticket1.TextoCentro(" ");
            Ticket1.LineasGuion();
            Ticket1.TextoExtremos("SOBRANTE:", corte["sobrante"]);
            Ticket1.TextoExtremos("GASTOS:", tgastos.ToString("0.00"));
            Ticket1.TextoExtremos("INGRESOS:", tingresos.ToString("0.00"));
            Ticket1.LineasGuion();
            Ticket1.TextoCentro(" ");
            Ticket1.TextoExtremos("TOTAL CZ:", totalCZ.ToString("0.00"));
            Ticket1.TextoCentro(" ");
            Ticket1.TextoCentro(" ");
            Ticket1.TextoCentro(" ");
            Ticket1.LineasGuion();
            Ticket1.TextoCentro("CAJERO:" + nc.ToUpper());

            Ticket1.CortaTicket();
        }
        private void Bimprimir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Imprimiendo corte");
            int selectedRowIndex = 1;
            if (selectedRowIndex < 0)
            {
                MessageBox.Show("Seleccione un corte de la lista.", "Advertencia");
                return;
            }
            Dictionary<string, string> corte = localDM.getCorte(selectedRowIndex);
            if (corte != null)
            {
                imprimirCorte(corte);
            }
            else
            {
                MessageBox.Show("No se pudo obtener la información del corte.", "Error");
            }
        }
    }
}
