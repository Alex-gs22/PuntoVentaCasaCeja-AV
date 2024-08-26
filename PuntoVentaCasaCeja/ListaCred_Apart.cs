using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel.Store;

namespace PuntoVentaCasaCeja
{
    public partial class ListaCred_Apart : Form
    {
        int rowCount, maxPages, currentPage = 1, offset, idCliente, rowsPerPage = 10;
        Action<Usuario> setUser;
        Usuario usuario;
        WebDataManager webDM;
        LocaldataManager localDM;
        CurrentData data;
        List<string> tipo = new List<string>();
        List<string> estados = new List<string>();
        string[] rangeTipo = { "Creditos", "Apartados" };
        string[] range = { "TODOS", "PENDIENTE", "EXPIRO", "CANCELADO", "PAGADO" };
        
        public ListaCred_Apart(CurrentData data)
        {
            InitializeComponent();
            this.webDM = data.webDM;
            this.data = data;
            this.localDM = webDM.localDM;
            tipo.AddRange(rangeTipo);
            BoxTipo.DataSource = tipo;
            BoxTipo.SelectedIndex = 0;
            estados.AddRange(range);
            BoxEstado.DataSource = estados;
            BoxEstado.SelectedIndex = 0;
            DataTable dataTable = localDM.GetCreditosDataTable(data.idSucursal);
            dataTable.DefaultView.Sort = "Fecha DESC";
            tablaCreditosApartados.DataSource = dataTable;
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
                        //tablaCreditos_CellDoubleClick(tablaCreditosApartados, new DataGridViewCellEventArgs(tablaCreditosApartados.CurrentCell.ColumnIndex, tablaCreditosApartados.CurrentCell.RowIndex));
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

        private void TablaCreditos_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.Enter)
            {
                //verificar si hay una fila seleccionada
                if (tablaCreditosApartados.CurrentCell == null)
                {
                    return;
                }
                DataGridViewCellEventArgs cellEventArgs = new DataGridViewCellEventArgs(tablaCreditosApartados.CurrentCell.ColumnIndex, tablaCreditosApartados.CurrentCell.RowIndex);
                tablaCreditos_CellDoubleClick(sender, cellEventArgs);
            }
            */
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
                DataGridViewRow selectedRow = tablaCreditosApartados.Rows[e.RowIndex];
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
            tablaCreditos_CellDoubleClick(tablaCreditosApartados, new DataGridViewCellEventArgs(tablaCreditosApartados.CurrentCell.ColumnIndex, tablaCreditosApartados.CurrentCell.RowIndex));
            tablaCreditosApartados.Focus();
        }

        private void BoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            offset = 0;
            FiltrarDatos();
        }

        private void BoxEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            offset = 0;
            FiltrarDatos();
        }

        private void FiltrarDatos()
        {
            DataTable dataTable;
            if (BoxTipo.SelectedIndex == 0) // muestra la tabla tal cual sin filtros
            {
                dataTable = localDM.GetCreditosDataTable(data.idSucursal);
            }
            else
            {
                dataTable = localDM.GetApartadosDataTable(data.idSucursal);
            }

            if (BoxEstado.SelectedItem != null && BoxEstado.SelectedIndex != 0) // Si no es "TODOS"
            {
                string estado = BoxEstado.SelectedItem.ToString(); // obtiene el estado seleccionado y lo convierte a string.
                dataTable.DefaultView.RowFilter = $"Estado = '{estado}'"; // Filtra la tabla por el estado seleccionado (es dinamico).
            }
            else
            {
                dataTable.DefaultView.RowFilter = string.Empty; // Si es "TODOS" no se filtra.
            }
            dataTable.DefaultView.Sort = "Fecha DESC"; // Ordena la tabla por fecha descendente.
            DataTable paginatedTable = dataTable.Clone(); // Clona la estructura de la tabla original.
            for (int i = offset; i < offset + rowsPerPage && i < dataTable.DefaultView.Count; i++) // Llena la tabla clonada con los datos de la tabla original.
            {
                paginatedTable.ImportRow(dataTable.DefaultView[i].Row); // Importa la fila de la tabla original a la tabla clonada.
            }

            tablaCreditosApartados.DataSource = paginatedTable;
            calculateMaxPages(dataTable.DefaultView.Count);
        }


        private void calculateMaxPages(int rowCount)
        {
            maxPages = (rowCount + rowsPerPage - 1) / rowsPerPage; // Divisón entera redondeando hacia arriba
            if (maxPages == 0)
                maxPages = 1;
            if (maxPages < currentPage)
            {
                currentPage = maxPages;
                offset = (currentPage - 1) * rowsPerPage;
            }
            pageLabel.Text = $"Página {currentPage}/{maxPages}";
        }

        private void prev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                offset -= rowsPerPage;
                currentPage--;
                FiltrarDatos();
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (currentPage < maxPages)
            {
                offset += rowsPerPage;
                currentPage++;
                FiltrarDatos();
            }
        }
    }
}
