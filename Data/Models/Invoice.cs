using Invoices.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Invoices.Data.Models
{
    public class Invoice
    {
        //Number – integer in range[1, 000, 000, 000…1, 500, 000, 000] (required)
        //IssueDate – DateTime(required)
        //DueDate – DateTime(required)
        //Amount – decimal (required)
        //CurrencyType – enumeration of type CurrencyType, with possible values(BGN, EUR, USD) (required)
        //ClientId – integer, foreign key(required)
        //Client – Client
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(typeof(int),"1000000000","1500000000")]
        public int Number { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public CurrencyType CurrencyType { get; set; }

        [Required]
        [ForeignKey(nameof(ClientId))]
        public int ClientId { get; set; }

         public Client Client { get; set; }

    }
}
