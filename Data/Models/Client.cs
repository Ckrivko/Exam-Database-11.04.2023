using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Invoices.Data.Models
{
    public class Client
    {
        //NumberVat – text with length[10…15] (required)
        //Invoices – collection of type Invoicе
        //Addresses – collection of type Address
        //ProductsClients – collection of type ProductClient

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25,MinimumLength =10)]
        public string Name { get; set; }

        [Required]
        [StringLength(15,MinimumLength =10)]
        public string NumberVat { get; set; }

        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();

        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();

        public ICollection<ProductClient> ProductsClients { get; set; } = new HashSet<ProductClient>();
    }
}
