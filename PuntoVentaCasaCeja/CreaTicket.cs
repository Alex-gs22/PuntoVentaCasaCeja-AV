﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoVentaCasaCeja
{
    // La clase "CreaTicket" tiene varios metodos para imprimir con diferentes formatos (izquierda, derecha, centrado, desripcion precio,etc), a
    // continuacion se muestra el metodo con ejemplo de parametro que acepta, longitud maxima y un ejemplo de como imprimira, esta clase esta
    // basada en una impresora Epson de matriz de puntos con impresion maxima de 40 caracteres por renglon
    // METODO                                      MAX_LONG                        EJEMPLOS
    //--------------------------------------------------------------------------------------------------------------------------
    // TextoIzquierda("Empleado 1")                    40                      Empleado 1      
    // TextoDerecha("Caja 1")                          40                                                        Caja 1
    // TextoCentro("Ticket")                           40                                         Ticket   
    // TextoExtremos("Fecha 6/1/2011","Hora:13:25")     18 y 18                 Fecha 6/1/2011                Hora:13:25
    // EncabezadoVenta()                                n/a                     Articulo        Can    P.Unit    Importe
    // LineasGuion()                                    n/a                     ----------------------------------------
    // AgregaArticulo("Aspirina","2",45.25,90.5)        16,3,10,11              Aspirina          2    $45.25     $90.50
    // LineasTotales()                                  n/a                                                ----------
    // AgregaTotales("Subtotal",235.25)                 25 y 15                Subtotal                         $235.25
    // LineasAsterisco()                                n/a                     ****************************************
    // LineasIgual()                                    n/a                     ========================================
    // CortaTicket()
    // AbreCajon()
    public class CreaTicket
    {
        string ticket = "";
        string parte1, parte2;
        public string impresora = ""; // nombre exacto de la impresora como esta en el panel de control
        int max, cort;

        public void TextoCentroCorte(string par1)
        {
            ticket = "";
            max = par1.Length;
            if (max > 43) // Ajustamos el límite a 43 caracteres
            {
                cort = max - 43;
                parte1 = par1.Remove(43, cort); // si es mayor que 43 caracteres, lo corta
            }
            else { parte1 = par1; }
            max = (int)(43 - parte1.Length) / 2;
            for (int i = 0; i < max; i++)
            {
                ticket += " ";
            }
            ticket += parte1 + "\n";
            RawPrinterHelper.SendStringToPrinter(impresora, "\x1B\x21\x10" + ticket); // Código de control para texto grande
        }

        public void TextoExtremosCorte(string par1, string par2)
        {
            max = par1.Length;
            if (max > 21) // Ajustamos el límite a 21 caracteres
            {
                cort = max - 21;
                parte1 = par1.Remove(21, cort); // si par1 es mayor que 21 lo corta
            }
            else { parte1 = par1; }
            ticket = parte1;
            max = par2.Length;
            if (max > 21) // Ajustamos el límite a 21 caracteres
            {
                cort = max - 21;
                parte2 = par2.Remove(21, cort); // si par2 es mayor que 21 lo corta
            }
            else { parte2 = par2; }
            max = 43 - (parte1.Length + parte2.Length);
            for (int i = 0; i < max; i++)
            {
                ticket += " ";
            }
            ticket += parte2 + "\n";
            RawPrinterHelper.SendStringToPrinter(impresora, "\x1B\x21\x10" + ticket); // Código de control para texto grande
        }

        public void AgregaTotalesCorte(string par1, double total)
        {
            max = par1.Length;
            if (max > 32) // Ajustamos el límite a 32 caracteres
            {
                cort = max - 32;
                parte1 = par1.Remove(32, cort); // si es mayor que 32 lo corta
            }
            else { parte1 = par1; }
            ticket = parte1;
            parte2 = total.ToString("c");
            max = 43 - (parte1.Length + parte2.Length);
            for (int i = 0; i < max; i++)
            {
                ticket += " ";
            }
            ticket += parte2 + "\n";
            RawPrinterHelper.SendStringToPrinter(impresora, "\x1B\x21\x10" + ticket); // Código de control para texto grande
        }

        public void LineasGuion()
        {
            ticket = "----------------------------------------\n";   // agrega lineas separadoras -
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
        }
        public void LineasAsterisco()
        {
            ticket = "****************************************\n";   // agrega lineas separadoras *
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
        }
        public void LineasIgual()
        {
            ticket = "========================================\n";   // agrega lineas separadoras =
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
        }
        public void LineasTotales()
        {
            ticket = "                             -----------\n"; ;   // agrega lineas de total
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
        }
        public void EncabezadoVenta()
        {
            ticket = "Articulo        Can    P.Unit    Importe\n";   // agrega lineas de  encabezados
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        }
        public void TextoIzquierda(string par1)                          // agrega texto a la izquierda
        {
            max = par1.Length;
            if (max > 40)                                 // **********
            {
                cort = max - 40;
                parte1 = par1.Remove(40, cort);        // si es mayor que 40 caracteres, lo corta
            }
            else { parte1 = par1; }                      // **********
            ticket = parte1 + "\n";
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        }
        public void TextoDerecha(string par1)
        {
            ticket = "";
            max = par1.Length;
            if (max > 40)                                 // **********
            {
                cort = max - 40;
                parte1 = par1.Remove(40, cort);           // si es mayor que 40 caracteres, lo corta
            }
            else { parte1 = par1; }                      // **********
            max = 40 - par1.Length;                     // obtiene la cantidad de espacios para llegar a 40
            for (int i = 0; i < max; i++)
            {
                ticket += " ";                          // agrega espacios para alinear a la derecha
            }
            ticket += parte1 + "\n";                    //Agrega el texto
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        }
        public void TextoCentro(string par1)
        {
            ticket = "";
            max = par1.Length;
            if (max > 40)                                 // **********
            {
                cort = max - 40;
                parte1 = par1.Remove(40, cort);          // si es mayor que 40 caracteres, lo corta
            }
            else { parte1 = par1; }                      // **********
            max = (int)(40 - parte1.Length) / 2;         // saca la cantidad de espacios libres y divide entre dos
            for (int i = 0; i < max; i++)                // **********
            {
                ticket += " ";                           // Agrega espacios antes del texto a centrar
            }                                            // **********
            ticket += parte1 + "\n";
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        }
        public void TextoExtremos(string par1, string par2)
        {
            max = par1.Length;
            if (max > 18)                                 // **********
            {
                cort = max - 18;
                parte1 = par1.Remove(18, cort);          // si par1 es mayor que 18 lo corta
            }
            else { parte1 = par1; }                      // **********
            ticket = parte1;                             // agrega el primer parametro
            max = par2.Length;
            if (max > 18)                                 // **********
            {
                cort = max - 18;
                parte2 = par2.Remove(18, cort);          // si par2 es mayor que 18 lo corta
            }
            else { parte2 = par2; }
            max = 40 - (parte1.Length + parte2.Length);
            for (int i = 0; i < max; i++)                 // **********
            {
                ticket += " ";                            // Agrega espacios para poner par2 al final
            }                                             // **********
            ticket += parte2 + "\n";                     // agrega el segundo parametro al final
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        }
        public void AgregaTotales(string par1, double total)
        {
            max = par1.Length;
            if (max > 25)                                 // **********
            {
                cort = max - 25;
                parte1 = par1.Remove(25, cort);          // si es mayor que 25 lo corta
            }
            else { parte1 = par1; }                      // **********
            ticket = parte1;
            parte2 = total.ToString("c");
            max = 40 - (parte1.Length + parte2.Length);
            for (int i = 0; i < max; i++)                // **********
            {
                ticket += " ";                           // Agrega espacios para poner el valor de moneda al final
            }                                            // **********
            ticket += parte2 + "\n";
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        }
        public void AgregaArticulo(string par1, int cant, double precio, double total)
        {
            if (cant.ToString().Length <= 3 && precio.ToString("c").Length <= 10 && total.ToString("c").Length <= 11) // valida que cant precio y total esten dentro de rango
            {
                max = par1.Length;
                if (max > 16)                                 // **********
                {
                    cort = max - 16;
                    parte1 = par1.Remove(16, cort);          // corta a 16 la descripcion del articulo
                }
                else { parte1 = par1; }                      // **********
                ticket = parte1;                             // agrega articulo
                max = (3 - cant.ToString().Length) + (16 - parte1.Length);
                for (int i = 0; i < max; i++)                // **********
                {
                    ticket += " ";                           // Agrega espacios para poner el valor de cantidad
                }
                ticket += cant.ToString();                   // agrega cantidad
                max = 10 - (precio.ToString("c").Length);
                for (int i = 0; i < max; i++)                // **********
                {
                    ticket += " ";                           // Agrega espacios
                }                                            // **********
                ticket += precio.ToString("c"); // agrega precio
                max = 11 - (total.ToString().Length);
                for (int i = 0; i < max; i++)                // **********
                {
                    ticket += " ";                           // Agrega espacios
                }                                            // **********
                ticket += total.ToString("c") + "\n"; // agrega precio
                RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
            }
            else
            {
                MessageBox.Show("Valores fuera de rango");
                RawPrinterHelper.SendStringToPrinter(impresora, "Error, valor fuera de rango\n"); // imprime texto
            }
        }
        public void CortaTicket()
        {
            string corte = "\x1B" + "m";                  // caracteres de corte
            string avance = "\x1B" + "d" + "\x09";        // avanza 9 renglones
            RawPrinterHelper.SendStringToPrinter(impresora, avance); // avanza
            RawPrinterHelper.SendStringToPrinter(impresora, corte); // corta
        }
        public void AbreCajon()
        {
            string cajon0 = "\x1B" + "p" + "\x00" + "\x0F" + "\x96";                  // caracteres de apertura cajon 0
            string cajon1 = "\x1B" + "p" + "\x01" + "\x0F" + "\x96";                 // caracteres de apertura cajon 1
            RawPrinterHelper.SendStringToPrinter(impresora, cajon0); // abre cajon0
            //RawPrinterHelper.SendStringToPrinter(impresora, cajon1); // abre cajon1
        }
    }
}
