using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ExportDto
{
    [XmlType("Invoice")]
    public class ExportInvoiceDto
    {
        //<InvoiceNumber>1063259096</InvoiceNumber>
        //<InvoiceAmount>167.22</InvoiceAmount>
        //<DueDate>02/19/2023</DueDate>
        //<Currency>EUR</Currency>

        [XmlElement("InvoiceNumber")]
        public int InvoiceNumber { get; set; }

        [XmlElement("InvoiceAmount")]
        public decimal InvoiceAmount { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [XmlElement("Currency")]
        public string Currency { get; set; }

    }
}
