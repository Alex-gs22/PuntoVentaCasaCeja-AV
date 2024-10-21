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
    public partial class CredApartSel : Form
    {
        CurrentData data;
        public CredApartSel(CurrentData data)
        {
            InitializeComponent();
            this.data = data;
        }

        private void credito_Click(object sender, EventArgs e)
        {
            ListaCred_Apart listaCr = new ListaCred_Apart(data);
            listaCr.ShowDialog();
        }
        private void BuscarCliente_Click(object sender, EventArgs e)
        {
            BuscarCliente buscarCl = new BuscarCliente(data);
            buscarCl.ShowDialog();
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
                        ListacreditosApartados.PerformClick();
                        break;
                    case Keys.F2:
                        BuscarCliente.PerformClick();
                        break;
                    case Keys.F3:
                        Bclientes.PerformClick();
                        break;
                    default:
                        return base.ProcessDialogKey(keyData);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void Bclientes_Click(object sender, EventArgs e)
        {
            ListaClientes listaCl = new ListaClientes(data);
            listaCl.ShowDialog();
        }
    }
}
