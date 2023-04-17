using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Invoices.DataProcessor.ImportDto
{
    [JsonObject]
    public class ImportInvoicesDto
    {
        [JsonProperty("Number")]
        [Required]
        [Range(typeof(int), "1000000000", "1500000000")]
        public int Number { get; set; }

        [JsonProperty("IssueDate")]
        [Required]
        public string IssueDate { get; set; }

        [JsonProperty("DueDate")]
        [Required]
        public string DueDate { get; set; }

        [JsonProperty("Amount")]
        [Required]
        public decimal Amount { get; set; }

        [JsonProperty("CurrencyType")]
        [Required]
        [Range(typeof(int),"0","2")]
        public int CurrencyType { get; set; }

        [JsonProperty("ClientId")]
        [Required]       
        public int ClientId { get; set; }

    }
}
