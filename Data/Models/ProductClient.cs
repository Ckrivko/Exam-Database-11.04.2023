using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Invoices.Data.Models
{
    public class ProductClient
    {
        [ForeignKey(nameof(ProductId))]
        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [ForeignKey(nameof(ClientId))]
        [Required]

        public int ClientId { get; set; }

        public Client Client { get; set; }

    }
}
