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
    public partial class IngresarMonto : Form
    {
        int tipo;
        Action<int, double> abono;
        Action<double> setTotal;
        double monto;
        public IngresarMonto(int tipo, Action<int, double> Abono, Action<double> SetTotal, double monto)
        {
            InitializeComponent();
            this.tipo = tipo;
            this.abono = Abono;
            this.setTotal = SetTotal;
            this.monto = monto;
        }
        private void numericInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void aceptar_Click(object sender, EventArgs e)
        {
            if (txtcantidad.Text.Equals("") || txtcantidad.Text.Equals("."))
            {
                MessageBox.Show("Favor de ingresar el monto", "Advertencia");
            }
            else
            {
                double m = double.Parse(txtcantidad.Text);
                if (m > 0)
                {
                    if(tipo != 0)
                    abono(tipo, m);
                    setTotal(m);
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El monto debe ser mayor a 0", "Advertencia");
                }
                
            }
        }

        private void cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void txtcantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                aceptar.PerformClick();
            }else if(e.KeyData == Keys.Escape)
            {
                cancelar.PerformClick();
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                        cancelar.PerformClick();
                        break;
                    case Keys.Enter:
                        aceptar.PerformClick();
                        break;
                    default:
                        return base.ProcessDialogKey(keyData);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void IngresarMonto_Load(object sender, EventArgs e)
        {
            if (tipo == 0)
            {
                this.Text = "Ingresar monto de apertura";
                groupBox1.Text = "MONTO DE APERTURA";
            } else if (tipo > 0)
            {
                this.Text="Ingresar pago";
                groupBox1.Text = "INGRESAR PAGO (" + monto.ToString("0.00") + ")";
            }
        }
    }
}
