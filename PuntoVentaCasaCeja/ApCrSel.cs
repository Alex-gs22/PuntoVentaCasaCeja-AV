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
    public partial class ApCrSel : Form
    {
        CurrentData data;
        public ApCrSel( CurrentData data )
        {
            InitializeComponent();
            this.data = data;
        }

        private void credito_Click(object sender, EventArgs e)
        {
            if (data.carrito.Count > 0)
            {
                RegistrarCredito rc = new RegistrarCredito(data);
                this.DialogResult = rc.ShowDialog(this);
                this.Close();
            }
            else
            {
                MessageBox.Show("Favor de agregar productos al carrito", "Advertencia");
            }
            
        }

        private void apartado_Click(object sender, EventArgs e)
        {
            if (data.carrito.Count > 0)
            {
                RegistrarApartado ra = new RegistrarApartado(data);
            this.DialogResult = ra.ShowDialog(this);
            this.Close();
            
            }
            else
            {
                MessageBox.Show("Favor de agregar productos al carrito", "Advertencia");
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
                    case Keys.F1:
                        credito.PerformClick();
                        break;
                    case Keys.F2:
                        apartado.PerformClick();
                        break;
                    case Keys.F3:
                        verCred.PerformClick();
                        break;
                    case Keys.F4:
                        verApa.PerformClick();
                        break;
                    default:
                        return base.ProcessDialogKey(keyData);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void verCred_Click(object sender, EventArgs e)
        {
            VerCredApa vca = new VerCredApa(0, data);
            vca.ShowDialog();
        }

        private void verApa_Click(object sender, EventArgs e)
        {
            VerCredApa vca = new VerCredApa(1, data);
            vca.ShowDialog();
        }
    }
}
