using System.Text.Json;
using DocumentFlow.Console.Models;

namespace DocumentFlow.Console;

public static class InvoiceFileReader
{
    // JsonSerializerDefaults.Web: nazwy w camelCase i brak rozróżniania wielkości liter,
    // więc "invoiceNumber" z JSON-a trafia do właściwości InvoiceNumber.
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public static List<Invoice> Read(string path)
    {
        string json = File.ReadAllText(path);

        List<Invoice>? invoices = JsonSerializer.Deserialize<List<Invoice>>(json, JsonOptions);

        if (invoices == null)
        {
            throw new JsonException("Nieprawidłowy plik: zawiera jedynie null");
        }

        return invoices;
    }
}
