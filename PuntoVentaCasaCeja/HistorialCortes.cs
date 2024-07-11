using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace PuntoVentaCasaCeja
{
    public partial class HistorialCortes : Form
    {
        Action<Usuario> setUser;
        Usuario usuario;
        WebDataManager webDM;
        LocaldataManager localDM;
        CurrentData data;

        public HistorialCortes(CurrentData data)
        {
            InitializeComponent();
            this.webDM = data.webDM;
            this.data = data;
            this.localDM = webDM.localDM;
        }

           protected override bool ProcessDialogKey(Keys keyData)
            {
            if (Form.ModifierKeys == Keys.None)
            {
                switch (keyData)
                {
                    case Keys.Enter:
                        BSelCorte.PerformClick();
                        break;
                    case Keys.Escape:
                        this.Close();
                        break;
                    case Keys.F5:
                        Bimprimir.PerformClick();
                        break;
                    case Keys.F6:
                        BelimHistorial.PerformClick();
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

        private void BSelCorte_Click(object sender, EventArgs e)
        {
            VerCorteHistorial verCorte = new VerCorteHistorial();
            verCorte.ShowDialog();
        }

        private void Bimprimir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Imprimiendo corte");
        }

        private void BelimHistorial_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Esta seguro que desea eliminar el historial de cortes?", "Eliminar historial", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                MessageBox.Show("Historial de cortes eliminado");
            }
        }

        private void tablaCortesZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BSelCorte.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.F5)
            {
                Bimprimir.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.F6)
            {
                BelimHistorial.PerformClick();
                return;
            }
        }

    }
}
