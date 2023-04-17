namespace Invoices.DataProcessor
{
    using Invoices.Data;
    using Invoices.DataProcessor.ExportDto;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            var sb = new StringBuilder();

            var root = new XmlRootAttribute("Clients");
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var xmlSerializer = new XmlSerializer(typeof(ExportClientDto[]), root);

            using (var writer = new StringWriter(sb))
            {

                var result = context.Clients
                    .Include(x => x.Invoices)
                    .ToArray()
                    .Where(x => x.Invoices.Any(a => a.IssueDate > date))
                    .Select(x => new ExportClientDto
                    {
                        InvoicesCount = x.Invoices.Count.ToString(),
                        ClientName = x.Name,
                        VatNumber = x.NumberVat,
                        Invoices = x.Invoices.
                        OrderBy(a => a.IssueDate).ThenByDescending(a => a.DueDate)
                        .Select(a => new ExportInvoiceDto
                        {
                            InvoiceNumber = a.Number,
                            InvoiceAmount = a.Amount,
                            Currency = a.CurrencyType.ToString(),
                            DueDate = a.DueDate.ToString("d",CultureInfo.InvariantCulture)

                        })
                        .ToArray()

                    })
                    .OrderByDescending(x => x.InvoicesCount)
                    .ThenBy(x => x.ClientName)
                    .ToArray();

                xmlSerializer.Serialize(writer, result,namespaces);

            }

            return sb.ToString().Trim();
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {
            var result = context.Products
                .Include(x => x.ProductsClients)
                .ThenInclude(x => x.Client)
                .ToArray()
                .Where(x => x.ProductsClients.Any(a => a.Client.Name.Length >= nameLength))
                .Select(x => new
                {
                    Name = x.Name,
                    Price = x.Price,
                    Category = x.CategoryType.ToString(),
                    Clients = x.ProductsClients.Where(a => a.Client.Name.Length >= nameLength)
                    .Select(a => new
                    {
                        Name = a.Client.Name,
                        NumberVat = a.Client.NumberVat
                    })
                    .OrderBy(x => x.Name)
                    .ToArray()

                })
                .OrderByDescending(x => x.Clients.Length)
                .ThenBy(x => x.Name)
                .Take(5)
                .ToArray();


            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }
    }
}