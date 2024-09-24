﻿using System;
using System.Windows.Forms;

namespace PuntoVentaCasaCeja
{
    public partial class BuscarCliente : Form
    {
        WebDataManager webDM;
        LocaldataManager localDM;
        bool encontrado = true;
        bool temporal = true;
        Cliente cliente;
        CurrentData data;

        public BuscarCliente(CurrentData data)
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
                        if (buscar.Focused || cancelar.Focused || buscar.Focused)
                            return base.ProcessDialogKey(keyData);
                        SendKeys.Send("{TAB}");
                        break;
                    case Keys.F5:
                        buscar.PerformClick();
                        break;
                    case Keys.F6:
                        otroCliente.PerformClick();
                        break;
                    default:
                        return base.ProcessDialogKey(keyData);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
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
            if (cliente != null && buscar.Text == "CONTINUAR (F5)")
            {
                data.cliente = cliente;
                ApCrSel sel = new ApCrSel(data);
                // Mostrar el diálogo y verificar el resultado antes de cerrar el formulario
                if (sel.ShowDialog(this) == DialogResult.OK)
                {
                    this.Close(); // Cerrar solo si el resultado fue OK
                }
            }
            if (encontrado)
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
                    encontrado = false;
                    buscar.Text = "CONTINUAR (F5)";
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

                    // Desactivar campos de entrada
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
                    txttel.Text = "";
                    txttel.Focus();
                }
            }
            else
            {
                encontrado = true;
                temporal = true;
                cliente = null;
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

                // Activar campos de entrada
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
                txttel.Focus();
            }
        }


        private void otroCliente_Click(object sender, EventArgs e)
        {
            encontrado = true;
            temporal = true;
            cliente = null;
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
            txttel.Focus();
            buscar.Text = "BUSCAR (F5)";
        }

        private void cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
    