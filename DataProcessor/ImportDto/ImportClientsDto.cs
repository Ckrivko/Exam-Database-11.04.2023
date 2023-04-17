using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType("Client")]
    public class ImportClientsDto
    {
        [XmlElement("Name")]
        [Required]
        [StringLength(25, MinimumLength = 10)]
        public string Name { get; set; }

        [XmlElement("NumberVat")]
        [Required]
        [StringLength(15, MinimumLength = 10)]
        public string NumberVat { get; set; }

        [XmlArray("Addresses")]
        public ImportClientAddressesDto[] Addresses { get; set; }
    }
}
