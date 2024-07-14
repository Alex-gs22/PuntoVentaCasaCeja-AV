using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PuntoVentaCasaCeja
{
    public partial class HistorialCortes : Form
    {
        Action<Usuario> setUser;
        Usuario usuario;
        WebDataManager webDM;
        LocaldataManager localDM;
        CurrentData data;

        public HistorialCortes(CurrentData data)
        {
            InitializeComponent();
            this.webDM = data.webDM;
            this.data = data;
            this.localDM = webDM.localDM;
            this.tablaCortesZ.DataSource = localDM.getCortes();
            this.tablaCortesZ.CellDoubleClick += new DataGridViewCellEventHandler(this.tablaCortesZ_CellDoubleClick);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None)
            {
                switch (keyData)
                {
                    case Keys.Enter:
                        BSelCorte.PerformClick();
                        break;
                    case Keys.Escape:
                        this.Close();
                        break;
                    case Keys.F5:
                        Bimprimir.PerformClick();
                        break;
                    case Keys.F6:
                        BelimHistorial.PerformClick();
                        break;
                    default:
                        return base.ProcessDialogKey(keyData);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BSelCorte_Click(object sender, EventArgs e)
        {
            VerCorteHistorial verCorte = new VerCorteHistorial();
            verCorte.ShowDialog();
            int selectedRowIndex = tablaCortesZ.CurrentCell.RowIndex;
            if (selectedRowIndex < 0)
            {
                MessageBox.Show("Seleccione un corte de la lista.", "Advertencia");
                return;
            }

            int idCorte = Convert.ToInt32(tablaCortesZ.Rows[selectedRowIndex].Cells["id"].Value);
            Dictionary<string, string> corteData = localDM.getCorte(idCorte);
            if (corteData != null)
            {
                /* VerCorteHistorial verCorte = new VerCorteHistorial(
                     corteData,
                     Convert.ToInt32(corteData["sucursal_id"]),
                     Convert.ToInt32(corteData["usuario_id"]),
                     idCorte,
                     Convert.ToInt32(corteData["idcaja"]),
                     localDM
                 );
                 var result = verCorte.ShowDialog();
                 if (result == DialogResult.Yes)
                 {
                     // Aquí puedes realizar acciones adicionales después de aceptar el corte
                     //MessageBox.Show("Corte completado exitosamente.", "Éxito");
                 }*/
                // Puedes agregar más lógica según sea necesario para otros resultados de DialogResult
            }
            else
            {
                MessageBox.Show("No se pudo obtener la información del corte.", "Error");
            }
        }

        private void tablaCortesZ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BSelCorte_Click(sender, e);
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
            int selectedRowIndex = tablaCortesZ.CurrentCell.RowIndex;
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

        private void BelimHistorial_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Esta seguro que desea eliminar el historial de cortes?", "Eliminar historial", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                MessageBox.Show("Historial de cortes eliminado");
                tablaCortesZ = new DataGridView();
                //realmente crees que sea necesario eliminar el historial de cortes?
            }
        }

        private void tablaCortesZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BSelCorte.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.F5)
            {
                Bimprimir.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.F6)
            {
                BelimHistorial.PerformClick();
                return;
            }
        }
    }
}
