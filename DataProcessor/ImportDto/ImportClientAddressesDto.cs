using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType("Address")]
    public class ImportClientAddressesDto
    {
        [XmlElement("StreetName")]
        [Required]
        [StringLength(20, MinimumLength = 10)]
        public string StreetName { get; set; }

        [XmlElement("StreetNumber")]
        [Required]
        public int StreetNumber { get; set; }

        [XmlElement("PostCode")]
        [Required]
        public string PostCode { get; set; }

        [XmlElement("City")]
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string City { get; set; }

        [XmlElement("Country")]
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Country { get; set; }

    }
}
