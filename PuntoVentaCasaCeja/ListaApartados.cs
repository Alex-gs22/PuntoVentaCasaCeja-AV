using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace PuntoVentaCasaCeja
{
    public partial class ListaApartados : Form
    {
        Action<Usuario> setUser;
        Usuario usuario;
        WebDataManager webDM;
        LocaldataManager localDM;
        CurrentData data;
        public ListaApartados(CurrentData data)
        {
            InitializeComponent();
            this.webDM = data.webDM;
            this.data = data;
            this.localDM = webDM.localDM;
            tablaApartados.DataSource = localDM.GetApartadosDataTable();
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
                       // aceptarButton.PerformClick();
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

        private void tablaApartados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tablaApartados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == tablaApartados.Columns["Cliente"].Index && e.RowIndex >= 0)
            {
                string clienteNombre = tablaApartados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                // Verificar y inicializar data.cliente si es null
                if (data.cliente == null)
                {
                    data.cliente = new Cliente();  // Asume que Cliente es la clase adecuada
                }

                int clienteId = localDM.GetCliente(clienteNombre);

                if (clienteId != -1) // Verifica si el cliente se encontró
                {
                    data.cliente.id = clienteId;
                    VerCredApa vca = new VerCredApa(1, data);
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
