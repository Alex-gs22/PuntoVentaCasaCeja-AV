using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Storage;
using Newtonsoft.Json;
using PuntoVentaCasaCeja.Properties;
using Windows.UI.Xaml;

namespace PuntoVentaCasaCeja
{
    public partial class VerOperaciones : Form
    {
        LocaldataManager localDM;
        DataTable data;
        bool firstTicket = false;
        int idcaja;
        string sucursalName, sucursalDir;
        List<ProductoVenta> productos;
        string ticket = "";
        string fontName = "";
        int fontSize;
        bool isValidTicket = false;
        string cajero;
        string fecha;
        string folio;
        string total ;
        double cambio;
        double totalformat;
        int printerType;
        int userlvl;
        Dictionary<string, double> pagos;
        Dictionary<int, float[]> tabs;
        private System.Drawing.Printing.PrintDocument docToPrint =
    new System.Drawing.Printing.PrintDocument();

        public VerOperaciones(LocaldataManager localdata, int idcaja, string sucursalName, string sucursalDir, int userlvl)
        {
            InitializeComponent();
            this.localDM = localdata;
            this.userlvl = userlvl;
            this.idcaja = idcaja;
            this.sucursalName = sucursalName;
            this.sucursalDir = sucursalDir;
            this.fontName = Settings.Default["fontName"].ToString();
            this.fontSize = int.Parse(Settings.Default["fontSize"].ToString());
            this.printerType = int.Parse(Settings.Default["printertype"].ToString());
            this.tabs = new Dictionary<int, float[]> () 
            {
                {5, new float[]{ 110, 30, 50, 50 } },
                {6, new float[]{ 130, 40, 60, 60 } },
                {7, new float[]{ 145, 45, 65, 65 } },
                {8, new float[]{ 160, 50, 65, 65 } },
                {9, new float[]{ 185, 55, 70, 70 } },
                {10, new float[]{ 210, 60, 75, 75 } },
                {11, new float[]{ 225, 75, 85, 85 } },
                {12, new float[]{ 250, 75, 90, 90 } },
                {13, new float[]{ 270, 80, 100, 100 } },
                {14, new float[]{ 290, 85, 110, 110 } },
                {15, new float[]{ 310, 90, 120, 120 } }
            };
            if (userlvl > 1)
            {
                delete.Enabled = false;
            }
        }

        private void VerOperaciones_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            string fechaSeleccionada = filtrarFecha.Value.ToString("yyyy-MM-dd");
            data = localDM.getVentasFecha(fechaSeleccionada);

            data.DefaultView.Sort = "id DESC";
            tabla.DataSource = data;
            txtbuscar.Focus();
            // Se comentó para que no se cargue el ticket automáticamente al cargar la ventana
            // loadTicket();
        }


        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtbuscar.Text.Equals(""))
            {
                string fechaSeleccionada = filtrarFecha.Value.ToString("yyyy-MM-dd");
                data = localDM.getVentasFecha(fechaSeleccionada);
            }
            else
            {
                data = localDM.getVentas(txtbuscar.Text);
            }
            tabla.DataSource = data;
            
            // Se comentó para que no se cargue el ticket automáticamente cuando el texto en buscar cambie
            // loadTicket();
        }

        private void loadTicket()
        {
            string piedeticket = Settings.Default["pieDeTicket"].ToString();
            ticket = "";
            isValidTicket = false;
            if (tabla.SelectedRows.Count > 0)
            {
                isValidTicket = true;
                cajero = tabla.SelectedRows[0].Cells[4].Value.ToString();
                fecha = tabla.SelectedRows[0].Cells[3].Value.ToString();
                folio = tabla.SelectedRows[0].Cells[2].Value.ToString();
                total = tabla.SelectedRows[0].Cells[1].Value.ToString();
                cambio = double.Parse(total);
                totalformat = double.Parse(total);
                pagos = JsonConvert.DeserializeObject<Dictionary<string, double>>(tabla.SelectedRows[0].Cells[5].Value.ToString());
                ticket += "CASA CEJA S.A. de C.V.\n" +
                    "SUCURSAL: " + sucursalName.ToUpper() + "\n" +
                    "" + sucursalDir.ToUpper() + "\n" +
                    "" + fecha + "\n" + 
                    "FOLIO: " + folio + "\n\n" +
                    "DESCRIPCION\tCANT\tP. UNIT\tP. TOTAL\n";
                    
                string id = tabla.SelectedRows[0].Cells[0].Value.ToString();
                productos = localDM.getProductosVenta(id);
                foreach(ProductoVenta p in productos)
                {
                    string n;
                    if (p.nombre.Length > 19)
                    {
                        n = p.nombre.Substring(0, 18);
                    }
                    else
                    {
                        n=p.nombre;
                    }
                    ticket +=  n + "\t" + p.cantidad + "\t" + p.precio_venta.ToString("0.00")+ "\t" + (p.cantidad*p.precio_venta).ToString("0.00")+"\n";
                }
                if (!fontName.Equals("Consolas"))
                    ticket += "--------------------";
                ticket += "--------------------------------------------------------------\n" +
                     "TOTAL $\t------>\t\t" + totalformat.ToString("0.00") + "\n";
                if (pagos.ContainsKey("debito"))
                {
                    ticket += "PAGO T. DEBITO\t------>\t\t" + pagos["debito"].ToString("0.00")+"\n";
                    cambio -= pagos["debito"];
                }
                if (pagos.ContainsKey("credito"))
                {
                    ticket += "PAGO T. CREDITO\t------>\t\t" + pagos["credito"].ToString("0.00") + "\n";
                    cambio -= pagos["credito"];
                }
                if (pagos.ContainsKey("cheque"))
                {
                    ticket += "PAGO CHEQUES\t------>\t\t" + pagos["cheque"].ToString("0.00") + "\n";
                    cambio -= pagos["cheque"];
                }
                if (pagos.ContainsKey("transferencia"))
                {
                    ticket += "PAGO TRANSFERENCIA\t------>\t\t" + pagos["transferencia"].ToString("0.00") + "\n";
                    cambio -= pagos["transferencia"];
                }
                if (pagos.ContainsKey("efectivo"))
                {
                    ticket += "EFECTIVO ENTREGADO\t------>\t\t" + pagos["efectivo"].ToString("0.00") + "\n";
                    cambio -= pagos["efectivo"];
                }
                ticket += "SU CAMBIO $\t------>\t\t" + (cambio * -1).ToString("0.00") + "\n";
                if (!fontName.Equals("Consolas"))
                    ticket += "--------------------";
                ticket += "--------------------------------------------------------------\n\n" +
                     "LE ATENDIO: " + cajero.ToUpper() + "\n" +
                     "NO DE ARTICULOS: "+productos.Count.ToString().PadLeft(5, '0')+"\n" +
                     "GRACIAS POR SU COMPRA\n\n" +
                     "ANTONIO CEJA MARON\n" +
                     "RFC: CEMA-721020-NM5\n\n";

                if (piedeticket != "")
                {
                    ticket += "----------------------------------------------------------------------------------\n" +
                    piedeticket + "\n" +
                    "----------------------------------------------------------------------------------\n\n";
                }

                ticket += "SI DESEA FACTURAR ESTA COMPRA INGRESE A :\n" +
                     "https://cm-papeleria.com/public/facturacion";
            }            
            createdoc();
        }
        private void createdoc()
        {
            
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "test.txt");
            // Construct the PrintPreviewControl.

            //// Set location, name, and dock style for printPreviewControl1.
            //this.printPreviewControl1.Name = "printPreviewControl1";

            // Set the Document property to the PrintDocument 
            // for which the PrintPage event has been handled.
            this.printPreviewControl1.Document = docToPrint;
            this.printPreviewControl1.Zoom = 2;
            if (fontSize > 6)
                this.printPreviewControl1.Zoom = 1.5;
            if (fontSize>10)
                this.printPreviewControl1.Zoom = 1.1;
            if (fontSize > 13)
                this.printPreviewControl1.Zoom = 1.0;
            // Set the document name. This will show be displayed when 
            // the document is loading into the control.
            this.printPreviewControl1.Document.DocumentName = path;
            this.printPreviewControl1.Document.PrinterSettings.PrinterName = localDM.impresora;

            // Set the UseAntiAlias property to true so fonts are smoothed
            // by the operating system.
            this.printPreviewControl1.UseAntiAlias = true;
            // Add the control to the form.

            // Associate the event-handling method with the
            // document's PrintPage event.
            this.docToPrint.PrintPage +=
                new System.Drawing.Printing.PrintPageEventHandler(
                docToPrint_PrintPage);
        }
        private void docToPrint_PrintPage(
    object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            // Insert code to render the page here.
            // This code will be called when the control is drawn.

            // The following code will render a simple
            // message on the document in the control.
            string text1 = ticket;
            //StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            //format.Alignment = StringAlignment.Center;
            //System.Drawing.Font printFont =
            //    new Font(fontName, fontSize, FontStyle.Regular);

            //e.Graphics.DrawString(text1, printFont,
            //    Brushes.Black, 50, 50);

            FontFamily fontFamily = new FontFamily(fontName);
            Font font = new Font(
               fontFamily,
               fontSize,
               FontStyle.Regular,
               GraphicsUnit.Point);
            Rectangle rect = new Rectangle(50, 50, 750, 1000);
            StringFormat stringFormat = new StringFormat();
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 0));
            

            stringFormat.SetTabStops(0, tabs[fontSize]);

            e.Graphics.DrawString(text1, font, solidBrush, rect, stringFormat);

            //Pen pen = Pens.Black;
            //e.Graphics.DrawRectangle(pen, rect);
        }
        private void tabla_KeyDown(object sender, KeyEventArgs e)
        {
            //loadTicket(); // carga la vista previa en automatico al moverse con las flechas.
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    //print.PerformClick();
                    // se comento para que no se imprima automaticamente al hacer enter en la tabla de ventas.
                    loadTicket(); // carga la vista previa del ticket al hacer Enter en la tabla de ventas.
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.F1:
                    txtbuscar.Focus();
                    break;
            }
        }

        private void tabla_MouseClick(object sender, MouseEventArgs e)
        {
            loadTicket(); // carga la vista previa del ticket al hacer click en la tabla de ventas.
        }
        private void txtbuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                tabla.Focus();
                SendKeys.Send("{DOWN}");
            }

            if (e.KeyData == Keys.Up)
            {
                tabla.Focus();
                SendKeys.Send("{UP}");
            }
            if (e.KeyData == Keys.Enter)
            {
                loadTicket(); // carga la vista previa del ticket al hacer Enter en el buscador.
                //print.PerformClick();
                // se comento para que no se imprima automaticamente al hacer enter en el buscador.
                e.Handled = true;
                e.SuppressKeyPress = true;
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
                        txtbuscar.Focus();
                        break;
                    case Keys.F5:
                        print.PerformClick();
                        break;
                    case Keys.F6:
                        delete.PerformClick();
                        break;
                    case Keys.Down:
                        tabla.Focus();
                        SendKeys.Send("{DOWN}");
                        break;
                    case Keys.Up:
                        tabla.Focus();
                        SendKeys.Send("{UP}");
                        break;
                    default:
                        return base.ProcessDialogKey(keyData);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabla_SelectionChanged(object sender, EventArgs e)
        {
            // esto se cambio para que no se cargue el ticket automaticamente al seleccionar una fila en la tabla de ventas
            //Asi solo muestra el primer ticket al cargar la ventana y despues solo se muestra al hacer click o enter en la tabla.
            if (!firstTicket)
            {
                loadTicket();
                firstTicket = true;
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DialogResult response = MessageBox.Show("¿Está seguro que desea limpiar el historial de ventas?", "Advertencia", MessageBoxButtons.YesNo);
            if(response == DialogResult.Yes)
            {
                localDM.limpiarVentas();
                loadData();
            }
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void filtrarFecha_ValueChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(filtrarFecha.Value.ToString("dd/MM/yyyy"));   
            loadData();
        }

        private void print_Click(object sender, EventArgs e)
        {
            if (isValidTicket)
            {
                if (printerType == 1)
                    printPreviewControl1.Document.Print();
                else
                {
                    Dictionary<string, string> venta = new Dictionary<string, string>();
                    venta["fecha_venta"] = fecha;
                    venta["folio"] = folio;
                    venta["total"] = total;
                    localDM.imprimirTicket(venta, productos, pagos, cajero, sucursalName, sucursalDir, true
                        );
                }
            }
            
        }
    }
}
