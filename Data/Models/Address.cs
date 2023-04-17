using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Invoices.Data.Models
{
    public class Address
    {
        //StreetName – text with length[10…20] (required)
        //StreetNumber – integer(required)
        //PostCode – text(required)
        //City – text with length[5…15] (required)
        //Country – text with length[5…15] (required)
       // ClientId – integer, foreign key(required)
       // Client – Client

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20,MinimumLength =10)]
        public string StreetName { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public string PostCode { get; set; }

        [Required]
        [StringLength(15,MinimumLength =5)]
        public string City { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Country { get; set; }

        [Required]
        [ForeignKey(nameof(ClientId))]
        public int ClientId { get; set; }

        public Client Client { get; set; }

    }
}
