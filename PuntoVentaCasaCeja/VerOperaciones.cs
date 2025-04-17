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
using System.Drawing.Printing;

namespace PuntoVentaCasaCeja
{
    public partial class VerOperaciones : Form
    {
        int rowCount, maxPages, currentPage, offset, idCliente, rowsPerPage = 10;
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
        string total;
        double cambio;
        double descuento;
        double totalformat;
        int printerType;
        int userlvl;
        Dictionary<string, double> pagos;
        Dictionary<int, float[]> tabs;
        BindingSource source = new BindingSource();
        private System.Drawing.Printing.PrintDocument docToPrint =
        new System.Drawing.Printing.PrintDocument();

        public VerOperaciones(LocaldataManager localdata, int idcaja, string sucursalName, string sucursalDir, int userlvl)
        {
            InitializeComponent();
            tabla.ColumnHeadersDefaultCellStyle.Font = new Font(tabla.Font.FontFamily, 18);
            tabla.RowsDefaultCellStyle.Font = new Font(tabla.Font.FontFamily, 16);
            offset = 0;
            currentPage = 1;
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
            loadTicket();
        }

        private void loadData()
        {
            string fechaSeleccionada = filtrarFecha.Value.ToString("yyyy-MM-dd");
            data = localDM.getVentasFecha(fechaSeleccionada);

            data.DefaultView.Sort = "id DESC";
            calculateMaxPages(data.Rows.Count);

            // Obtener las filas para la página actual
            var paginatedRows = data.AsEnumerable().Skip(offset).Take(rowsPerPage);

            if (paginatedRows.Any())
            {
                var paginatedData = paginatedRows.CopyToDataTable();
                // Asignar el DataSource al BindingSource para que administre las actualizaciones
                source.DataSource = paginatedData;
                tabla.DataSource = source;
            }
            else
            {
                // Manejar el caso donde no hay filas para mostrar
                source.DataSource = null;
                tabla.DataSource = source;
            }

            txtbuscar.Focus();
            // Se comentó para que no se cargue el ticket automáticamente al cargar la ventana
             loadTicket();
        }


        private void calculateMaxPages(int rowCount)
        {
            maxPages = ((rowCount % rowsPerPage) == 0) ? rowCount / rowsPerPage : rowCount / rowsPerPage + 1;
            if (maxPages == 0)
                maxPages++;
            if (maxPages < currentPage)
            {
                currentPage = maxPages;
                offset = (currentPage - 1) * rowsPerPage;
            }
            pageLabel.Text = "Página " + currentPage + "/" + maxPages;
        }
        private void prev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                offset -= rowsPerPage;
                currentPage--;
                loadData();
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (currentPage < maxPages)
            {
                offset += rowsPerPage;
                currentPage++;
                loadData();
            }
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
                data = localDM.getVentas(txtbuscar.Text);
            }
            tabla.DataSource = data;
            
            // Se comentó para que no se cargue el ticket automáticamente cuando el texto en buscar cambie
            // loadTicket();
        }
        private void obtenerDescuento()
        {
            if (tabla.SelectedRows.Count > 0)
            {
                var selectedRow = tabla.SelectedRows[0];
                var descuentoValue = selectedRow.Cells[2].Value;
                if (double.TryParse(descuentoValue.ToString(), out double descuento))
                {
                    this.descuento = descuento;
                }
                else
                {
                    MessageBox.Show("El valor de la celda de descuento no es un número válido.", "Error de conversión");
                    this.descuento = 0;
                }
            }
        }
        private void loadTicket()
        {
            obtenerDescuento();
            string piedeticket = Settings.Default["pieDeTicket"].ToString();
            ticket = "";
            isValidTicket = false;
            if (tabla.SelectedRows.Count > 0)
            {
                isValidTicket = true;
                cajero = tabla.SelectedRows[0].Cells[5].Value.ToString();
                fecha = tabla.SelectedRows[0].Cells[4].Value.ToString();
                folio = tabla.SelectedRows[0].Cells[3].Value.ToString();
                total = tabla.SelectedRows[0].Cells[1].Value.ToString();

                cambio = double.Parse(total);
                cambio -= descuento;
                totalformat = double.Parse(total);
                pagos = JsonConvert.DeserializeObject<Dictionary<string, double>>(tabla.SelectedRows[0].Cells[6].Value.ToString());
                ticket += "CASA CEJA\n" +
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
                if (descuento>0)                
                {
                    ticket += "SE APLICO DESCUENTO DE $\t------>\t\t" + descuento.ToString("0.00") + "\n";
                }
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
            // Configura el tamaño de papel según el tipo de impresora
            if (printerType == 0) // Ticket
            {
                // 78 mm ≈ 3.07 pulgadas; en PaperSize se usa hundredths of an inch, entonces 3.07*100 ≈ 307.
                // La altura se define de forma arbitraria, ajústala según la longitud de tu ticket.
                PaperSize ticketSize = new PaperSize("Ticket", 465, 1169);
                docToPrint.DefaultPageSettings.PaperSize = ticketSize;
                // Establecemos márgenes en 0 para aprovechar todo el ancho.
                docToPrint.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
            }
            else if (printerType == 1)// Papel tamaño carta
            {
                // Por ejemplo, tamaño carta: 8.5 x 11 pulgadas.
                // En hundredths of an inch: ancho=850, alto=1100.
                PaperSize letterSize = new PaperSize("Letter", 850, 1100);
                docToPrint.DefaultPageSettings.PaperSize = letterSize;
                // Márgenes más amplios para papel carta (los puedes ajustar a tu preferencia)
                docToPrint.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
            }

            // Establecemos la ruta y configuramos el control de vista previa de impresión.
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "reimprimirVenta.txt");
            this.printPreviewControl1.Document = docToPrint;

            // Ajustamos el Zoom del control según el tamaño de fuente (estas condiciones son de ejemplo)
            this.printPreviewControl1.Zoom = 2;
            if (fontSize > 6)
                this.printPreviewControl1.Zoom = 1.5;
            if (fontSize > 10)
                this.printPreviewControl1.Zoom = 1.1;
            if (fontSize > 13)
                this.printPreviewControl1.Zoom = 1.0;

            // Definimos el nombre del documento y seleccionamos la impresora según la configuración
            this.printPreviewControl1.Document.DocumentName = path;
            this.printPreviewControl1.Document.PrinterSettings.PrinterName = localDM.impresora;

            // Utilizamos anti alias para suavizar las fuentes
            this.printPreviewControl1.UseAntiAlias = true;

            // Asociamos el evento PrintPage para el documento
            this.docToPrint.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(docToPrint_PrintPage);
        }

        private void docToPrint_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Contenido del ticket (por ejemplo, variable 'ticket')
            string text1 = ticket;

            // Creamos la fuente con el nombre y tamaño configurados
            FontFamily fontFamily = new FontFamily(fontName);
            Font font = new Font(fontFamily, fontSize, FontStyle.Regular, GraphicsUnit.Point);

            // Usamos e.MarginBounds para definir el área de impresión según la configuración del tamaño de papel y márgenes
            Rectangle rect = e.MarginBounds;

            // Configuramos el formato de la cadena para gestionar tabulaciones (si aplica)
            StringFormat stringFormat = new StringFormat();
            stringFormat.SetTabStops(0, tabs[fontSize]);

            // Usamos un pincel sólido para dibujar el texto en negro
            SolidBrush solidBrush = new SolidBrush(Color.Black);

            // Dibujamos el contenido en el área definida
            e.Graphics.DrawString(text1, font, solidBrush, rect, stringFormat);
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
                try
                {
                    if (printerType == 1)
                        printPreviewControl1.Document.Print();
                    else
                    {
                        double descuento = double.Parse(tabla.SelectedRows[0].Cells[2].Value.ToString());
                        bool esDescuento = descuento > 0;
                        Dictionary<string, string> venta = new Dictionary<string, string>();
                        venta["fecha_venta"] = fecha;
                        venta["folio"] = folio;
                        venta["total"] = total;
                        cambio = double.Parse(total) - descuento;
                        localDM.imprimirTicket(venta, productos, pagos, cajero, sucursalName, sucursalDir,
                            true, cambio, esDescuento, descuento);
                    }
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    MessageBox.Show("No se guardo el PDF, ya se encuentra abierto un documento con el mismo nombre.", "Error");
                }
            }
        }
      
    }
}
