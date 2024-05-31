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
    public partial class ListaCreditos : Form
    {
        Action<Usuario> setUser;
        Usuario usuario;
        WebDataManager webDM;
        LocaldataManager localDM;
        CurrentData data;
        public ListaCreditos(CurrentData data)
        {
            InitializeComponent();
            this.webDM = data.webDM;
            this.data = data;
            this.localDM = webDM.localDM;
            tablaCreditos.DataSource = localDM.GetCreditosDataTable();
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
                    case Keys.F5:
                        //aceptarButton.PerformClick();
                        break;
                    case Keys.F6:
                        //eliminarButton.PerformClick();
                        break;
                    case Keys.Enter:
                        //aceptarButton.PerformClick();
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

        private void tablaCreditos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == tablaCreditos.Columns["Cliente"].Index && e.RowIndex >= 0)
            {
                string clienteNombre = tablaCreditos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (data.cliente == null)
                {
                    data.cliente = new Cliente();
                }

                int clienteId = localDM.GetCliente(clienteNombre);

                if (clienteId != -1)
                {
                    data.cliente.id = clienteId;
                    VerCredApa vca = new VerCredApa(0, data);
                    vca.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
