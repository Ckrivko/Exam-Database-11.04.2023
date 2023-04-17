using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Invoices.DataProcessor.ImportDto
{
    [JsonObject]
    public class ImportProductsDto
    {
        [JsonProperty("Name")]
        [Required]
        [StringLength(30, MinimumLength = 9)]
        public string Name { get; set; }

        [JsonProperty("Price")]
        [Required]
        [Range(typeof(decimal), "5.00", "1000.00")]
        public decimal Price { get; set; }

        [JsonProperty("CategoryType")]
        [Range(typeof(int),"0","4")]
        public int CategoryType { get; set; }

        [JsonProperty("Clients")]
        public int[] Clients { get; set; }


    }
}
