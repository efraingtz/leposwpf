using LeposWPF.Helpers.Clases;
using LeposWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LeposWPF.Helpers
{
    public class TicketPOS
    {

        private static void printTicket(ref Ticket ticket) 
        {
            Company company = CompanyHelper.currentCompany;
            if (ticket.PrinterExists(company.Printer))
                ticket.PrintTicket(company.Printer);
            else MessageBox.Show("La Impresora No Esta Conectada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      
        }

        //Venta
        public static void TicketVenta(Sale sale, String clientName)
        {
            Company empresa = CompanyHelper.currentCompany;
            Ticket ticket = getTicket(clientName);
            ticket.AddHeaderLine(empresa.Name);
            ticket.AddHeaderLine(empresa.Description);
            ticket.AddHeaderLine(empresa.Address);
            ticket.AddSubHeaderLine("Venta # " + sale.ID);
            ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

            foreach (var row in sale.SaleProducts)
            {
                String cantidad = row.Quantity+"";
                String descripcion =row.Product.Description;
                double precio = row.Price;
                ticket.AddItem(cantidad, descripcion, precio.ToString("C"));
            }

            double total = sale.Total;
            double descuento = 1.0 - (sale.Discount/ 100.0);
            double subtotal = sale.Total / descuento;
            double siva;
            double iva = 0;
            int ivaconf = sale.IVAType;
            switch (ivaconf)
            {
                case 1:
                    subtotal = sale.Total / descuento;
                    siva = subtotal * descuento;
                    iva = total / 1.16 * 0.16;
                    break;
                case 2:
                    subtotal = (sale.Total / 1.16) / descuento;
                    iva = total / 1.16 * 0.16;
                    break;
            }
            String tipo = !sale.IsCredit ? "Contado" : "Crédito";
            ticket.AddTotal("Tipo Venta", tipo);
            ticket.AddTotal("SUBTOTAL", subtotal.ToString("C"));
            ticket.AddTotal("% Descuento", sale.Discount.ToString());
            if(ivaconf!=0)
                ticket.AddTotal("IVA", iva.ToString("C"));
            ticket.AddTotal("TOTAL", sale.Total.ToString("C"));
           
            ticket.AddTotal("", "");
            ticket.AddFooterLine("VUELVA PRONTO");
            printTicket(ref ticket);
        }

     
        //Venta
        public static void TicketVenta(DataGridView dgvVenta, Sale venta, String recibo, String cambio, String clientName)
        {
            Company empresa = CompanyHelper.currentCompany;
            Ticket ticket = getTicket(clientName);
            ticket.AddSubHeaderLine("Venta # " + venta.ID);
            ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            foreach(DataGridViewRow row in dgvVenta.Rows)
            {
                String cantidad = row.Cells[2].Value.ToString();
                String descripcion = row.Cells[1].Value.ToString();
                double precio = double.Parse(row.Cells[3].Value.ToString());
                ticket.AddItem(cantidad, descripcion, precio.ToString("C"));
            }
            double total = venta.Total;
            double descuento = 1.0 - (venta.Discount / 100.0);
            double subtotal = venta.Total / descuento;
            double siva; 
            double iva = 0;
            int ivaconf = venta.IVAType;
            switch (ivaconf)
            {
                case 1:
                    siva = subtotal * descuento;
                    iva = total / 1.16 * 0.16;
                    break;
                case 2:
                    subtotal = (venta.Total / 1.16) / descuento;
                    iva = total / 1.16 * 0.16;
                    break;
            }
            String tipo = !venta.IsCredit ? "Contado" : "Crédito";
            ticket.AddTotal("Tipo Venta", tipo);
            ticket.AddTotal("SUBTOTAL", subtotal.ToString("C"));
            ticket.AddTotal("% Descuento", venta.Discount.ToString());
            if (ivaconf != 0)
                ticket.AddTotal("IVA", iva.ToString("C"));
           if (!venta.IsCredit)
            {
                ticket.AddTotal("TOTAL", venta.Total.ToString("C"));
                ticket.AddTotal("Recibo", double.Parse(recibo).ToString("C"));
                ticket.AddTotal("Cambio", cambio);
            }
            else 
            {
                ticket.AddTotal("Adeudo", venta.Total.ToString("C"));
                ticket.AddHeaderLine("Fecha Máxima de Pago");
                DateTime fechaVenta = DateTime.Now;
                DateTime fc = fechaVenta.AddDays(venta.CreditDays);
                ticket.AddHeaderLine(fc.ToString());
            }
            ticket.AddTotal("", "");
            ticket.AddFooterLine("VUELVA PRONTO");
            printTicket(ref ticket);
        }
        

        private static Ticket getTicket(String cliente)
        {
            Company empresa = CompanyHelper.currentCompany;
            Ticket ticket = new Ticket();
            ticket.MaxChar = int.Parse(empresa.CharsPerLine);
            ticket.FontSize =int.Parse(empresa.FontSize);
            ticket.AddHeaderLine(empresa.Name);
            ticket.AddHeaderLine(empresa.Description);
            ticket.AddHeaderLine(empresa.Address);
            ticket.AddHeaderLine("RFC: " + empresa.RFC);
            ticket.AddHeaderLine("Cliente:");
            ticket.AddHeaderLine(cliente);

            return ticket;
        }

        //AbonoVenta
        public static void TicketAbonoVentaCredito(SalePayment abono, Sale venta, Double deuda, String clientName)
        {
            Company empresa = CompanyHelper.currentCompany;
            Ticket ticket = getTicket(clientName);

            ticket.AddSubHeaderLine("Venta # " + venta.ID);
            ticket.AddSubHeaderLine("Abono # " + abono.ID);
            ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

            ticket.AddTotal("Total Venta", venta.Total.ToString("C"));
            ticket.AddTotal("Deuda:", deuda.ToString("C"));
            ticket.AddTotal("Abono:", abono.Payment.ToString("C"));
            ticket.AddTotal("Saldo:", (deuda-abono.Payment).ToString("C"));
            ticket.AddTotal("", "");

            ticket.AddFooterLine("VUELVA PRONTO");
            printTicket(ref ticket);
        }

    }
}
