using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PuntoVentaCasaCeja
{
    public partial class ListaCred_Apart : Form
    {
        private int rowCount = 0;
        private int maxPages = 0;
        private int currentPage = 1;
        private int offset = 0;
        private int idCliente = 0;
        private int rowsPerPage = 10;

        private Usuario usuario;
        private WebDataManager webDM;
        private LocaldataManager localDM;
        private CurrentData data;

        private List<string> tipo = new List<string>();
        private List<string> estados = new List<string>();

        private readonly string[] rangeTipo = { "Creditos", "Apartados" };
        private readonly string[] range = { "TODOS", "PENDIENTE", "EXPIRO", "CANCELADO", "PAGADO" };

        public ListaCred_Apart(CurrentData data)
        {
            InitializeComponent();
            this.data = data;
            this.webDM = data.webDM;
            this.localDM = webDM.localDM;

            // Inicialización de ComboBoxes
            tipo.AddRange(rangeTipo);
            BoxTipo.DataSource = tipo;
            BoxTipo.SelectedIndex = 0;

            estados.AddRange(range);
            BoxEstado.DataSource = estados;
            BoxEstado.SelectedIndex = 0;

            // Configuración inicial de la tabla
            CargarDatosTabla();
        }

        private void CargarDatosTabla()
        {
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
                        ShowDropDown(BoxTipo);
                        break;
                    case Keys.F2:
                        ShowDropDown(BoxEstado);
                        break;
                    default:
                        return base.ProcessDialogKey(keyData);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void ShowDropDown(ComboBox comboBox)
        {
            comboBox.DroppedDown = true;
            comboBox.Focus();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TablaCreditos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                ShowDropDown(BoxTipo);
            if (e.KeyCode == Keys.F2)
                ShowDropDown(BoxEstado);
        }

        private void tablaCreditos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow selectedRow = tablaCreditosApartados.Rows[e.RowIndex];
                    string clienteNombre = selectedRow.Cells["Cliente"].Value.ToString();

                    if (data.cliente == null)
                        data.cliente = new Cliente();

                    int clienteId = localDM.GetCliente(clienteNombre);

                    if (clienteId != -1)
                    {
                        data.cliente.id = clienteId;
                        VerCredApa vca = new VerCredApa(BoxTipo.SelectedIndex, data);
                        vca.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al procesar la acción: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BSelCliente_Click(object sender, EventArgs e)
        {
            if (tablaCreditosApartados.CurrentCell != null)
            {
                tablaCreditos_CellDoubleClick(tablaCreditosApartados, new DataGridViewCellEventArgs(tablaCreditosApartados.CurrentCell.ColumnIndex, tablaCreditosApartados.CurrentCell.RowIndex));
                tablaCreditosApartados.Focus();
            }
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
            // Obtener los datos según el tipo seleccionado
            DataTable dataTable = (BoxTipo.SelectedIndex == 0) ?
                localDM.GetCreditosDataTable(data.idSucursal) :
                localDM.GetApartadosDataTable(data.idSucursal);

            // Aplicar filtro de estado si es necesario
            if (BoxEstado.SelectedItem != null && BoxEstado.SelectedIndex != 0)
            {
                string estado = BoxEstado.SelectedItem.ToString();
                dataTable.DefaultView.RowFilter = $"Estado = '{estado}'";
            }
            else
            {
                dataTable.DefaultView.RowFilter = string.Empty;
            }
            dataTable.DefaultView.Sort = "Fecha DESC";

            // Verificar que haya datos antes de paginar
            if (dataTable.DefaultView.Count > 0)
            {
                var paginatedRows = dataTable.DefaultView.ToTable().AsEnumerable()
                    .Skip(offset).Take(rowsPerPage);

                DataTable paginatedTable = paginatedRows.Any() ? paginatedRows.CopyToDataTable() : dataTable.Clone();

                tablaCreditosApartados.DataSource = paginatedTable;
            }
            else
            {
                tablaCreditosApartados.DataSource = dataTable.Clone(); // Vacío si no hay datos
            }

            CalculateMaxPages(dataTable.DefaultView.Count);
        }

        private void CalculateMaxPages(int rowCount)
        {
            maxPages = (rowCount + rowsPerPage - 1) / rowsPerPage;
            if (maxPages == 0) maxPages = 1;
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
