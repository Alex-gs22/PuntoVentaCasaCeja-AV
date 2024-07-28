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
    public partial class ListaCred_Apart : Form
    {
        Action<Usuario> setUser;
        Usuario usuario;
        WebDataManager webDM;
        LocaldataManager localDM;
        CurrentData data;
        List<string> estados = new List<string>();
        string[] tipo = { "Creditos", "Apartados" };
        string[] range = { "PENDIENTE", "EXPIRO", "CANCELADO", "PAGADO", "TODOS" };
        
        public ListaCred_Apart(CurrentData data)
        {
            InitializeComponent();
            this.webDM = data.webDM;
            this.data = data;
            this.localDM = webDM.localDM;
            BoxTipo.Items.AddRange(tipo);
            BoxTipo.SelectedIndex = 0;
            estados.AddRange(range);
            BoxEstado.DataSource = estados;
            BoxEstado.SelectedIndex = 0;
            tablaCreditos.DataSource = localDM.GetCreditosDataTable(data.idSucursal);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                        this.Close();
                        break;
                    case Keys.F1:
                        BoxTipo.DroppedDown = true;
                        BoxTipo.Focus();
                        break;
                    case Keys.F2:
                        BoxEstado.DroppedDown = true;
                        BoxEstado.Focus();
                        break;
                    case Keys.Enter:
                        tablaCreditos_CellDoubleClick(tablaCreditos, new DataGridViewCellEventArgs(tablaCreditos.CurrentCell.ColumnIndex, tablaCreditos.CurrentCell.RowIndex));
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

        private void tablaCreditos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TablaCreditos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //verificar si hay una fila seleccionada
                if (tablaCreditos.CurrentCell == null)
                {
                    return;
                }
                DataGridViewCellEventArgs cellEventArgs = new DataGridViewCellEventArgs(tablaCreditos.CurrentCell.ColumnIndex, tablaCreditos.CurrentCell.RowIndex);
                tablaCreditos_CellDoubleClick(sender, cellEventArgs);
            }
            if (e.KeyCode == Keys.F1)
            {
                BoxTipo.DroppedDown = true;
                BoxTipo.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                BoxEstado.DroppedDown = true;
                BoxEstado.Focus();
            }
        }

        private void tablaCreditos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = tablaCreditos.Rows[e.RowIndex];
                string clienteNombre = selectedRow.Cells["Cliente"].Value.ToString();

                if (data.cliente == null)
                {
                    data.cliente = new Cliente();
                }

                int clienteId = localDM.GetCliente(clienteNombre);

                if (clienteId != -1)
                {
                    data.cliente.id = clienteId;
                    if (BoxTipo.SelectedIndex == 0)
                    {
                        VerCredApa vca = new VerCredApa(0, data);
                        vca.ShowDialog();
                    }
                    else
                    {
                        VerCredApa vca = new VerCredApa(1, data);
                        vca.ShowDialog();
                    }

                }
                else
                {
                    MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BSelCliente_Click(object sender, EventArgs e)
        {   
            tablaCreditos_CellDoubleClick(tablaCreditos, new DataGridViewCellEventArgs(tablaCreditos.CurrentCell.ColumnIndex, tablaCreditos.CurrentCell.RowIndex));
            tablaCreditos.Focus();
        }

        private void BoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BoxTipo.SelectedIndex == 0)
            {
                tablaCreditos.DataSource = localDM.GetCreditosDataTable(data.idSucursal);
                tablaCreditos.Focus();
            }
            else
            {
                tablaCreditos.DataSource = localDM.GetApartadosDataTable(data.idSucursal);
                tablaCreditos.Focus();
            }
        }

        private void BoxEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
