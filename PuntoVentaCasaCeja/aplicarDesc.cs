using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Media.Core;
using Windows.UI.Xaml;

namespace PuntoVentaCasaCeja
{
    public partial class aplicarDesc : Form
    {
        private List<string> tipo = new List<string>();
        private readonly string[] rangeTipo = { "PORCENTAJE", "CANTIDAD" };
        private Action<double> setTotal;
        double total;
        bool esDescuento;

        public aplicarDesc(Action<double> setTotal, double total, bool esDescuento)
        {
            InitializeComponent();
            this.total = total;
            this.setTotal = setTotal;
            txtDescuento.Text = "0.00";
            tipo.AddRange(rangeTipo);
            BoxTipo.DataSource = tipo;
        }

        private void Bsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aceptar_Click(object sender, EventArgs e)
        {
            calcularDesc(esDescuento);            
            this.Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.aceptar.PerformClick();
                return true;
            }
            if (keyData == Keys.Escape)
            {
                this.Bsalir.PerformClick();
                return true;
            }
            if (keyData == Keys.F1)
            {
                BoxTipo.Focus();
                BoxTipo.DroppedDown = true;
                return true;
            }
            if (keyData == Keys.F2)
            {
                txtDescuento.Focus();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void BoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BoxTipo.SelectedIndex == 0)
            {
                label1.Text = "%";
            }
            else
            {
                label1.Text = "$";
            }
        }

        private void calcularDesc(bool esDescuento)
        {
            double descuento = 0;
            if (double.TryParse(txtDescuento.Text, out double valordescuento))
            {
                if (BoxTipo.SelectedIndex == 0)
                {
                    descuento = total * (valordescuento / 100);
                    MessageBox.Show("porcentaje "+descuento.ToString());
                }
                else
                {                    
                    descuento = valordescuento;
                    MessageBox.Show("cantidad "+descuento.ToString());
                }
                setTotal(descuento);
            }
            esDescuento = true;
            MessageBox.Show("clase aplicar desc " + esDescuento);
        }
    }
}
