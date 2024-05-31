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
            ListaCreditos listaCr = new ListaCreditos(data);
            listaCr.ShowDialog();
        }

        private void apartado_Click(object sender, EventArgs e)
        {
            ListaApartados listaAp = new ListaApartados(data);
            listaAp.ShowDialog();
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
                        BuscarCliente.PerformClick();
                        break;
                    case Keys.F4:
                        ListaClientes.PerformClick();
                        break;
                    default:
                        return base.ProcessDialogKey(keyData);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void BuscarCliente_Click(object sender, EventArgs e)
        {
            BuscarCliente buscarCl = new BuscarCliente(data);
            buscarCl.ShowDialog();
        }

        private void ListaClientes_Click(object sender, EventArgs e)
        {
            ListaClientes listaCl = new ListaClientes(data);
            listaCl.ShowDialog();
        }
    }
}
