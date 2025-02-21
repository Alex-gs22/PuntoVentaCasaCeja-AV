using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.UI.Xaml;

namespace PuntoVentaCasaCeja
{
    public partial class ListaClientes : Form
    {
        Action<Usuario> setUser;
        Usuario usuario;
        WebDataManager webDM;
        LocaldataManager localDM;
        CurrentData data;
        AltaCliente altaCliente;
        static Usuario activador = null;
        static Usuario admin = null;
        bool baja = true;
        public ListaClientes(CurrentData data)
        {
            InitializeComponent();
            this.webDM = data.webDM;
            this.data = data;
            this.localDM = webDM.localDM;
            this.tablaClientes.DataSource = localDM.getClientes();
            this.altaCliente = new AltaCliente(data);
            tablaClientes.ColumnHeadersDefaultCellStyle.Font = new Font(tablaClientes.Font.FontFamily, 14);
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
                        altaButton.PerformClick();
                        break;
                    case Keys.F6:
                        modificarButton.PerformClick();
                        break;
                    case Keys.F7:
                        BajaButton.PerformClick();
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
        bool pedirAutorizacion()
        {
            UserLogin login = new UserLogin(localDM, setActivador, true);
            DialogResult response = login.ShowDialog();
            if (response == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                this.Dispose();
                return false;
            }
        }
        void setActivador(Usuario usuario)
        {
            activador = usuario;
        }
        void setAdmin(Usuario usuario)
        {
            admin = usuario;
            webDM.activeUser = usuario;
        }
        
        private async void BajaButton_Click(object sender, EventArgs e)
        {
            if (tablaClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("No se ha seleccionado ningun cliente");
                return;
            }

            UserLogin login = new UserLogin(localDM, setAdmin, true);
            DialogResult result = login.ShowDialog();

            if (result == DialogResult.Yes)
            {
                int idCliente = Convert.ToInt32(tablaClientes.SelectedRows[0].Cells[0].Value);

                // Llamar al método para desactivar el cliente
                var desactivarResult = await webDM.DesactivarClienteAsync(idCliente);

                if (desactivarResult["status"] == "success")
                {
                    MessageBox.Show("Cliente dado de baja correctamente");
                    localDM.eliminarCliente(idCliente);
                    localDM.eliminarClienteTemporal(idCliente);
                    tablaClientes.DataSource = localDM.getClientes();
                }
                else
                {
                    MessageBox.Show($"Error al desactivar el cliente: {desactivarResult["message"]}");
                }

                tablaClientes.Focus();
            }
            else
            {
                MessageBox.Show("Autenticacion Fallida");
            }
        }


        private void seleccion(object sender, EventArgs e)
        {
            if (tablaClientes.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(tablaClientes.SelectedRows[0].Cells[0].Value); 
                Cliente cliente = localDM.getCliente(id);
                altaCliente.clienteSeleccionado(cliente);
                if (data.successful)
                {
                    this.Close();
                }
            }
        }

        private void seleccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                seleccion(sender, e);
            }
        }

        private void tablaClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                seleccion_KeyDown(sender, e);
            }
        }

        private void tablaClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = tablaClientes.Rows[e.RowIndex];
                tablaClientes.ClearSelection();
                row.Selected = true;
            }
        }

        private void modificarButton_Click(object sender, EventArgs e)
        {
            if (tablaClientes.SelectedRows.Count > 0)
            {
                Cliente cliente = new Cliente();
                cliente.nombre = tablaClientes.SelectedRows[0].Cells[1].Value.ToString();
                cliente.telefono = tablaClientes.SelectedRows[0].Cells[2].Value.ToString();
                cliente.correo = tablaClientes.SelectedRows[0].Cells[3].Value.ToString();
                cliente.rfc = tablaClientes.SelectedRows[0].Cells[4].Value.ToString();
                cliente.numero_exterior = tablaClientes.SelectedRows[0].Cells[5].Value.ToString();
                cliente.numero_interior = tablaClientes.SelectedRows[0].Cells[6].Value.ToString();
                cliente.codigo_postal = tablaClientes.SelectedRows[0].Cells[7].Value.ToString();
                cliente.calle = tablaClientes.SelectedRows[0].Cells[8].Value.ToString();
                cliente.colonia = tablaClientes.SelectedRows[0].Cells[9].Value.ToString();
                cliente.ciudad = tablaClientes.SelectedRows[0].Cells[10].Value.ToString();
                cliente.id = Convert.ToInt32(tablaClientes.SelectedRows[0].Cells[0].Value);
                ModificarCliente modCliente = new ModificarCliente(data, cliente);
                modCliente.ShowDialog();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningún cliente.");
            }
            tablaClientes.DataSource = localDM.getClientes();
        }

        private void tablaClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
   
        }

        private void altaButton_Click(object sender, EventArgs e)
        {
            AltaCliente altaCliente = new AltaCliente(data);
            altaCliente.ShowDialog();
            tablaClientes.DataSource = localDM.getClientes();
        }

        private void BSelCliente_Click(object sender, EventArgs e)
        {
            seleccion(sender, e);
            tablaClientes.Focus();
        }
    }
}