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
    public partial class AltaCliente : Form
    {
        WebDataManager webDM;
        LocaldataManager localDM;
        bool alta = true;
        bool temporal = true;
        Cliente cliente;
        CurrentData data;
        public AltaCliente( CurrentData data)
        {
            InitializeComponent();
            this.webDM = data.webDM;
            this.data = data;
            this.localDM = webDM.localDM;
        }
        private void integerInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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
                    case Keys.Enter:
                        if (aceptar.Focused || cancelar.Focused || buscar.Focused)
                            return base.ProcessDialogKey(keyData);
                        SendKeys.Send("{TAB}");
                        break;
                    case Keys.F5:
                        aceptar.PerformClick();
                        break;
                    case Keys.F6:
                        buscar.PerformClick();
                        break;
                    default:
                        return base.ProcessDialogKey(keyData);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aceptar_Click(object sender, EventArgs e)
        {
            if (alta)
            {
                if (txtnombre.Text.Equals("") || txttel.Text.Equals("") || txtcorreo.Text.Equals(""))
                {
                    MessageBox.Show("Favor de completar los campos obligatorios (*)", "Advertencia");
                }
                else
                {
                    int id = localDM.ExisteCliente(txtnombre.Text, txtcorreo.Text, txttel.Text);
                    if (id == -1)
                    {

                        NuevoCliente cl = new NuevoCliente()
                        {
                            nombre = txtnombre.Text,
                            rfc = txtrfc.Text,
                            calle = txtcalle.Text,
                            numero_exterior = txtnoext.Text,
                            numero_interior = txtnoint.Text,
                            colonia = txtcolonia.Text,
                            codigo_postal = txtpostal.Text,
                            ciudad = txtciudad.Text,
                            telefono = txttel.Text,
                            correo = txtcorreo.Text,
                        };
                        id = localDM.clienteTemporal(cl);
                        cliente = new Cliente
                        {
                            id=id,
                            nombre = txtnombre.Text,
                            rfc = txtrfc.Text,
                            calle = txtcalle.Text,
                            numero_exterior = txtnoext.Text,
                            numero_interior = txtnoint.Text,
                            colonia = txtcolonia.Text,
                            codigo_postal = txtpostal.Text,
                            ciudad = txtciudad.Text,
                            telefono = txttel.Text,
                            correo = txtcorreo.Text,
                            activo = -1
                        };
                        ClearAllText(this);
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un cliente con estos datos", "Advertencia");
                    }

                }
            }
            if (cliente != null)
            {
                //abrir el selector clientes/apartados pasando creditos
                data.cliente = cliente;
                ApCrSel sel = new ApCrSel(data);
                this.DialogResult = sel.ShowDialog(this);
                this.Close();
            }
        }
        async void send(NuevoCliente cliente)
        {
            Dictionary<string, string> result = await webDM.SendClienteAsync(cliente);
            MessageBox.Show(result["message"], "Estado: "+result["status"]);

        }
        void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else
                    ClearAllText(c);
            }
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            if (alta)
            {
                if (!txttel.Text.Equals(""))
                {
                    cliente = localDM.buscarCliente(txttel.Text);
                }
                else if (!txtcorreo.Text.Equals(""))
                {
                    cliente = localDM.buscarCliente(txtcorreo.Text);
                }
                else if (!txtnombre.Text.Equals(""))
                {
                    cliente = localDM.buscarCliente(txtnombre.Text);
                }
                if (cliente != null)
                {
                    if (cliente.activo != -1)
                        temporal = false;
                    alta = false;
                    buscar.Text = "OTRO CLIENTE (F6)";
                    aceptar.Text = "CONTINUAR (F5)";
                    txtnombre.Text = cliente.nombre;
                    txtrfc.Text = cliente.rfc;
                    txttel.Text = cliente.telefono;
                    txtcalle.Text = cliente.calle;
                    txtcorreo.Text = cliente.correo;
                    txtnoext.Text = cliente.numero_exterior;
                    txtnoint.Text = cliente.numero_interior;
                    txtcolonia.Text = cliente.colonia;
                    txtpostal.Text = cliente.codigo_postal;
                    txtciudad.Text = cliente.ciudad;

                    txtnombre.Enabled = false;
                    txtrfc.Enabled = false;
                    txttel.Enabled = false;
                    txtcalle.Enabled = false;
                    txtcorreo.Enabled = false;
                    txtnoext.Enabled = false;
                    txtnoint.Enabled = false;
                    txtcolonia.Enabled = false;
                    txtpostal.Enabled = false;
                    txtciudad.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado", "Advertencia");
                }
            }
            else
            {
                alta = true;
                temporal = true;
                cliente = null;
                buscar.Text = "BUSCAR (F6)";
                aceptar.Text = "ALTA DE CLIENTE (F5)";
                txtnombre.Text = "";
                txtrfc.Text = "";
                txttel.Text = "";
                txtcalle.Text = "";
                txtcorreo.Text = "";
                txtnoext.Text = "";
                txtnoint.Text = "";
                txtcolonia.Text = "";
                txtpostal.Text = "";
                txtciudad.Text = "";
                txtnombre.Enabled = true;
                txtrfc.Enabled = true;
                txttel.Enabled = true;
                txtcalle.Enabled = true;
                txtcorreo.Enabled = true;
                txtnoext.Enabled = true;
                txtnoint.Enabled = true;
                txtcolonia.Enabled = true;
                txtpostal.Enabled = true;
                txtciudad.Enabled = true;
                txtnombre.Focus();
            }
            
        }
    }
}
