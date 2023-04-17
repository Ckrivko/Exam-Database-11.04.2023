using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ExportDto
{
    [XmlType("Client")]
    public class ExportClientDto
    {
        //InvoicesCount="9">
        //<ClientName>SPEDOX,SRO</ClientName>
        //<VatNumber>SK2023911087</VatNumber>
        //<Invoices>
        
        [XmlAttribute("InvoicesCount")]
        public string InvoicesCount { get; set; }

        [XmlElement("ClientName")]
        public string ClientName { get; set; }

        [XmlElement("VatNumber")]
        public string VatNumber { get; set; }

        [XmlArray("Invoices")]
        public ExportInvoiceDto[] Invoices { get; set; }

    }
}
