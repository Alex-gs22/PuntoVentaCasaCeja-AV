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
using Newtonsoft.Json;
using Windows.Storage;
using PuntoVentaCasaCeja.Properties;

namespace PuntoVentaCasaCeja
{
    public partial class RegistrarApartado : Form
    {
        WebDataManager webDM;
        LocaldataManager localDM;
        List<ProductoVenta> carrito;
        double totalcarrito;
        int idsucursal, idcaja;
        string folio, sucursalName, sucursalDir;
        DateTime localDate;
        Dictionary<string, double> pagos;
        bool reprint = false;
        double totalpagado = 0;
        int dias = 0;
        Usuario cajero;
        bool isValidTicket = false;
        string ticket;
        string fontName;
        string foliocorte;
        Cliente cliente;
        int fontSize, printerType, idcorte;
        Dictionary<int, float[]> tabs;
        PrintPreviewControl printPreviewControl1;
        private System.Drawing.Printing.PrintDocument docToPrint =
    new System.Drawing.Printing.PrintDocument();
        public RegistrarApartado(CurrentData data)
        {
            InitializeComponent();
            this.cliente = data.cliente;
            this.webDM = data.webDM;
            this.localDM = webDM.localDM;
            this.carrito = data.carrito;
            this.totalcarrito = data.totalcarrito;
            this.sucursalName = data.sucursalName;
            this.sucursalDir = data.sucursalDir;
            this.idcorte = data.idCorte;
            idsucursal = int.Parse(Settings.Default["sucursalid"].ToString());
            idcaja = int.Parse(Settings.Default["posid"].ToString());
            this.localDate = DateTime.Now;
            this.fontName = Settings.Default["fontName"].ToString();
            this.fontSize = int.Parse(Settings.Default["fontSize"].ToString());
            printerType = int.Parse(Settings.Default["printertype"].ToString());
            pagos = new Dictionary<string, double>();
            this.cajero = webDM.activeUser;
            printPreviewControl1 = new PrintPreviewControl();
            this.folio = idsucursal.ToString().PadLeft(2, '0') + idcaja.ToString().PadLeft(2, '0') + localDate.Day.ToString().PadLeft(2, '0') + localDate.Month.ToString().PadLeft(2, '0') + localDate.Year + "A";
            this.tabs = new Dictionary<int, float[]>()
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
        }

        private void aceptar_Click(object sender, EventArgs e)
        {
            if (txtnombre.Text.Equals("") || txtcorreo.Text.Equals("") || txttel.Text.Equals("") || txtdias.Text.Equals(""))
            {
                MessageBox.Show("Favor de completar los campos requeridos", "Advertencia");
            }
            else
            {
                Apartado na = new Apartado()
                {
                    productos = JsonConvert.SerializeObject(carrito),
                    total = totalcarrito,
                    total_pagado = totalpagado,
                    folio = this.folio,
                    fecha_de_apartado = localDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    estado = 0,
                    cliente_creditos_id = cliente.id,
                    temporal = cliente.activo == -1 ? 1 : 0,
                    id_cajero_registro = webDM.activeUser.id,
                    fecha_entrega = null,
                    id_cajero_entrega = null,
                    sucursal_id = idsucursal,
                    observaciones = txtobservaciones.Text,

            };
                
               
                int id = localDM.apartadoTemporal(na);
                this.folio += id.ToString().PadLeft(4, '0');
                na.folio = folio;
                na.abonos = new List<AbonoApartado>();
                if (pagos.Count > 0)
                {
                    AbonoApartado abono = new AbonoApartado
                    {
                        fecha = localDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        folio = idsucursal.ToString().PadLeft(2, '0') + idcaja.ToString().PadLeft(2, '0') + localDate.Day.ToString().PadLeft(2, '0') + localDate.Month.ToString().PadLeft(2, '0') + localDate.Year + "AA",
                        folio_corte = foliocorte,
                        apartado_id = 0,
                        usuario_id = webDM.activeUser.id,
                        folio_apartado = folio,
                        metodo_pago = JsonConvert.SerializeObject(pagos),
                        total_abonado = totalpagado,
                    };
                   
                    int ida = localDM.abonoApartadoTemporal(abono);                    
                    na.abonos.Add(abono);
                    localDM.acumularPagos(pagos, idcorte);
                    if (pagos.ContainsKey("efectivo"))
                    {
                        localDM.acumularEfectivoApartado(pagos["efectivo"], idcorte);
                    }
                    
                }
                txtfolio.Text = folio;
                imprimirTicketCarta(localDate.ToString("dd/MM/yyyy hh:mm tt"));
                imprimirTicketCarta(localDate.ToString("dd/MM/yyyy hh:mm tt"));
                if (localDM.impresora.Equals(""))
                {
                    MessageBox.Show("No se ha establecido una impresora", "Advertencia");
                }
                else
                {
                    if (printerType==1)
                    {
                        printPreviewControl1.Document.Print();
                        if (reprint)
                        {
                            printPreviewControl1.Document.Print();
                        }
                    }

                    else
                    {
                        localDM.imprimirApartado(na, carrito, pagos, cajero.nombre, sucursalName, sucursalDir, txtfecha.Text);
                        localDM.imprimirApartado(na, carrito, pagos, cajero.nombre, sucursalName, sucursalDir, txtfecha.Text);
                        if (reprint)
                        {
                            localDM.imprimirApartado(na, carrito, pagos, cajero.nombre, sucursalName, sucursalDir, txtfecha.Text);
                        }
                    }
                }
                
                send(na);
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            
        }
        async void send(Apartado apartado){
            Dictionary<string, string> result = await webDM.SendapartadoAsync(apartado);
            MessageBox.Show(result["message"], "Estado: " + result["status"]);

            if (result["status"] == "success")
            {
                List<ProductoVenta> productos = carrito;

                if (productos == null || productos.Count == 0)
                {
                    MessageBox.Show("El carrito está vacío", "Error");
                    return;
                }

                foreach (ProductoVenta p in productos)
                {
                    //Console.WriteLine($"Restando existencia para producto ID: {p.id}, cantidad: {p.cantidad}");
                    await webDM.restarExistencia(idsucursal, p.id, p.cantidad);
                }
            }
            else
            {
                MessageBox.Show("No es posible realizar esta operacion ahora", "Error");
            }
        }


        private void RegistrarApartado_Load(object sender, EventArgs e)
        {
            
            folio = idsucursal.ToString().PadLeft(2, '0') + idcaja.ToString().PadLeft(2, '0') + localDate.Day.ToString().PadLeft(2, '0') + localDate.Month.ToString().PadLeft(2, '0') + localDate.Year + "A";
            txtfolio.Text = folio;
            txtnombre.Text = cliente.nombre;
            txttel.Text = cliente.telefono;
            txtcorreo.Text = cliente.telefono;
            txttotal.Text = totalcarrito.ToString("0.00");
            txtabonado.Text = "0.00";
            txtpagoentrega.Text = totalcarrito.ToString("0.00");
        }

        private void txtdias_TextChanged(object sender, EventArgs e)
        {
            dias = txtdias.Text.Equals("")?0: int.Parse(txtdias.Text);
            DateTime d = localDate.AddDays(dias);
            txtfecha.Text = d.Day.ToString().PadLeft(2, '0') + "/" + d.Month.ToString().PadLeft(2, '0') + "/" + d.Year;
        }

        private void integerInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        void abono(int tipo, double cantidad)
        {
            switch (tipo)
            {
                case 1:
                    if (pagos.ContainsKey("efectivo"))
                    {
                        pagos["efectivo"] += cantidad;
                    }
                    else
                    {
                        pagos["efectivo"] = cantidad;
                    }
                    break;
                case 2:
                    if (pagos.ContainsKey("debito"))
                    {
                        pagos["debito"] += cantidad;
                    }
                    else
                    {
                        pagos["debito"] = cantidad;
                        reprint = true;
                    }
                    break;
                case 3:
                    if (pagos.ContainsKey("credito"))
                    {
                        pagos["credito"] += cantidad;
                    }
                    else
                    {
                        pagos["credito"] = cantidad;
                        reprint = true;
                    }
                    break;
                case 4:
                    if (pagos.ContainsKey("cheque"))
                    {
                        pagos["cheque"] += cantidad;
                    }
                    else
                    {
                        pagos["cheque"] = cantidad;
                        reprint = true;
                    }
                    break;
                case 5:
                    if (pagos.ContainsKey("transferencia"))
                    {
                        pagos["transferencia"] += cantidad;
                    }
                    else
                    {
                        pagos["transferencia"] = cantidad;
                        reprint = true;
                    }
                    break;
            }
            totalpagado += cantidad;
            txtabonado.Text = totalpagado.ToString("0.00");
            txtpagoentrega.Text = (totalcarrito - totalpagado).ToString("0.00");
        }

        private void abonar_Click(object sender, EventArgs e)
        {
            MetodoPago mp = new MetodoPago(totalcarrito-totalpagado, abono);
            mp.ShowDialog();
        }

        private void txtfolio_TextChanged(object sender, EventArgs e)
        {

        }

        private void cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
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
                    case Keys.Enter:
                        if (aceptar.Focused || cancelar.Focused || abonar.Focused)
                            return base.ProcessDialogKey(keyData);
                        SendKeys.Send("{TAB}");
                        break;
                    case Keys.F5:
                        aceptar.PerformClick();
                        break;
                    case Keys.F6:
                        abonar.PerformClick();
                        break;
                    default:
                        return base.ProcessDialogKey(keyData);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void imprimirTicketCarta(string fecha)
        {
            ticket = "";
            string caj = cajero.nombre;
            ticket += "CASA CEJA\n" +
                "SUCURSAL: " + sucursalName.ToUpper() + "\n" +
                "" + sucursalDir.ToUpper() + "\n" +
                "" + fecha + "\n" +
                "FOLIO: " + folio + "\n" +
                "TICKET DE APARTADO\n\n" +
                 "DESCRIPCION\tCANT\tP. UNIT\tP. TOTAL\n";
            foreach (ProductoVenta p in carrito)
            {
                string n;
                if (p.nombre.Length > 19)
                {
                    n = p.nombre.Substring(0, 18);
                }
                else
                {
                    n = p.nombre;
                }
                ticket += n + "\t" + p.cantidad + "\t" + p.precio_venta.ToString("0.00") + "\t" + (p.cantidad * p.precio_venta).ToString("0.00") + "\n";
            }
            if (!fontName.Equals("Consolas"))
                ticket += "--------------------";
            ticket += "--------------------------------------------------------------\n" +
                 "TOTAL $\t------>\t\t" + totalcarrito.ToString("0.00") + "\n";
            if (pagos.ContainsKey("debito"))
            {
                ticket += "PAGO T. DEBITO\t------>\t\t" + pagos["debito"].ToString("0.00") + "\n";
            }
            if (pagos.ContainsKey("credito"))
            {
                ticket += "PAGO T. CREDITO\t------>\t\t" + pagos["credito"].ToString("0.00") + "\n";
            }
            if (pagos.ContainsKey("cheque"))
            {
                ticket += "PAGO CHEQUES\t------>\t\t" + pagos["cheque"].ToString("0.00") + "\n";
            }
            if (pagos.ContainsKey("transferencia"))
            {
                ticket += "PAGO TRANSFERENCIA\t------>\t\t" + pagos["transferencia"].ToString("0.00") + "\n";
            }
            if (pagos.ContainsKey("efectivo"))
            {
                ticket += "EFECTIVO ENTREGADO\t------>\t\t" + pagos["efectivo"].ToString("0.00") + "\n";
            }
            if (!fontName.Equals("Consolas"))
                ticket += "--------------------";
            ticket += "--------------------------------------------------------------\n" +
                "POR PAGAR $\t------>\t\t" + (totalcarrito - totalpagado).ToString("0.00") + "\n\n" +
                 "LE ATENDIO: " + cajero.nombre.ToUpper() + "\n" +
                 "NO DE ARTICULOS: " + carrito.Count.ToString().PadLeft(5, '0') + "\n" +
                 "FECHA DE VENCIMIENTO:\n"+txtfecha.Text+"\n"+
                 "CLIENTE:\n"+cliente.nombre+"\n"+
                 "NUMERO DE CELULAR:\n"+cliente.telefono+"\n";
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
            if (fontSize > 10)
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
    }
}
