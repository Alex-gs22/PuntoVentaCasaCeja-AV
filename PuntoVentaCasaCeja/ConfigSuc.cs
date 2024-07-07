using PuntoVentaCasaCeja.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoVentaCasaCeja
{
    public partial class ConfigSuc : Form
    {

        public string SelectedSucursal => boxsucursal.SelectedItem.ToString();
        public string IdCaja => txtid.Text;
        public string PieTicket => txtPieTicket.Text;
        LocaldataManager localDM;
        Dictionary<string, int> mapasucursales;
        List<string> sucursales;
        List<string> listfont;
        List<int> listSizes;
        public ConfigSuc(LocaldataManager localdata)
        {
            InitializeComponent();
            this.localDM = localdata;
            mapasucursales = localDM.getIndicesSucursales();
            sucursales = new List<string>(mapasucursales.Keys);
            boxsucursal.DataSource = sucursales;
            listfont = new List<string>();
            listSizes = new List<int> { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.TextLength > 5)
            {
                // Si el texto supera los 50 caracteres, lo recorta a 50 caracteres
                textBox.Text = textBox.Text.Substring(0, 5);
                // Mueve el cursor al final del texto
                textBox.SelectionStart = textBox.TextLength;

                MessageBox.Show("Se excedio el limite de caracteres", "Advertencia");
            }
        }

        private void integerInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || (textBox.Text.Length >= 2 && !char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void Bsalir_Click(object sender, EventArgs e)
        {
            this.Close();
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
                        Baceptar.PerformClick();
                        break;
                    default:
                        return base.ProcessDialogKey(keyData);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void ConfigSuc_Load(object sender, EventArgs e)
        {
            string key = mapasucursales.FirstOrDefault(x => x.Value == int.Parse(Settings.Default["sucursalid"].ToString())).Key;
            boxsucursal.SelectedIndex = sucursales.IndexOf(key) == -1 ? 0 : sucursales.IndexOf(key);
            txtid.Text = Settings.Default["posid"].ToString();
        }

        private void Baceptar_Click(object sender, EventArgs e)
        {
            // con estas lineas se deberia de guardar directamente al presionar aceptar y no al presionar guardar en ConfigWindow.
            //Settings.Default["txtPieTicket"] = txtPieTicket.Text;
            //Settings.Default.Save();

            if (txtid.Text.Equals(""))
            {
                MessageBox.Show("No se ha establecido el ID de caja", "Advertencia");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
