namespace Invoices.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Invoices.Data;
    using Invoices.Data.Models;
    using Invoices.Data.Models.Enums;
    using Invoices.DataProcessor.ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";

        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";

        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";


        public static string ImportClients(InvoicesContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var root = new XmlRootAttribute("Clients");
            var xmlSerializer = new XmlSerializer(typeof(ImportClientsDto[]), root);

            var result = new List<Client>();


            using (var reader = new StringReader(xmlString))
            {
                var importClientDto = (ImportClientsDto[])xmlSerializer.Deserialize(reader);

                foreach (var clientDto in importClientDto)
                {
                    if (!IsValid(clientDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }


                    var currClient = new Client
                    {
                        Name = clientDto.Name,
                        NumberVat = clientDto.NumberVat,

                    };

                    foreach (var addressDto in clientDto.Addresses)
                    {
                        if (!IsValid(addressDto))
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        var currAddress = new Address
                        {
                            StreetName = addressDto.StreetName,
                            StreetNumber = addressDto.StreetNumber,
                            PostCode = addressDto.PostCode,
                            City = addressDto.City,
                            Country = addressDto.Country,

                        };

                        currClient.Addresses.Add(currAddress);
                    }

                    sb.AppendLine(String.Format(SuccessfullyImportedClients, currClient.Name));
                    result.Add(currClient);

                }
                context.Clients.AddRange(result);
                context.SaveChanges();
            }

            return sb.ToString().Trim();
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var importInvoicesDto = JsonConvert.DeserializeObject<List<ImportInvoicesDto>>(jsonString);
            var result = new List<Invoice>();

            foreach (var invoiceDto in importInvoicesDto)
            {
                if (!IsValid(invoiceDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }               

                var isIssueDateValid = DateTime.TryParseExact
                 (invoiceDto.IssueDate, "yyyy-MM-ddTHH:mm:ss",
                  CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime issueDate);

                var dueDateValid = DateTime.TryParseExact
                (invoiceDto.DueDate, "yyyy-MM-ddTHH:mm:ss",
                 CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDate);

                if (!isIssueDateValid ||
                    !isIssueDateValid ||
                    dueDate < issueDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var currInvoice = new Invoice
                {
                    Number = invoiceDto.Number,
                    IssueDate = issueDate,
                    DueDate = dueDate,
                    Amount = invoiceDto.Amount,
                    CurrencyType = (CurrencyType)invoiceDto.CurrencyType,
                    ClientId = invoiceDto.ClientId

                };

                sb.AppendLine(String.Format(SuccessfullyImportedInvoices, currInvoice.Number));
                result.Add(currInvoice);
            }
            context.Invoices.AddRange(result);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var importProductsDto = JsonConvert.DeserializeObject<List<ImportProductsDto>>(jsonString);
            var result = new List<Product>();

            var clientsId = context.Clients.Select(x => x.Id).ToHashSet<int>();


            foreach (var productDto in importProductsDto)
            {
                if (!IsValid(productDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var currProduct = new Product
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    CategoryType = (CategoryType)productDto.CategoryType

                };

                foreach (var id in productDto.Clients.Distinct())
                {
                    if (!clientsId.Contains(id))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var currProductClient = new ProductClient
                    {
                        ClientId = id,
                        ProductId = currProduct.Id
                    };

                    currProduct.ProductsClients.Add(currProductClient);

                }

                sb.AppendLine(String.Format
                    (SuccessfullyImportedProducts, currProduct.Name, currProduct.ProductsClients.Count));
                result.Add(currProduct);

            }
            context.Products.AddRange(result);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
