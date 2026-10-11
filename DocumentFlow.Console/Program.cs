using System.Text.Json;
using DocumentFlow.Console;
using DocumentFlow.Console.Models;

// AppContext.BaseDirectory to katalog z plikiem .dll (bin/Debug/net10.0/),
// dzięki czemu ścieżka działa niezależnie od tego, skąd uruchomiono program.
string path = Path.Combine(AppContext.BaseDirectory, "data", "invoices.json");

if (!File.Exists(path))
{
    Console.WriteLine($"Błąd: nie znaleziono pliku z fakturami: {path}");
    return 1;
}

List<Invoice> invoices;
try
{
    invoices = InvoiceFileReader.Read(path);
}
catch (JsonException ex)
{
    Console.WriteLine($"Błąd: plik {path} zawiera nieprawidłowy JSON.");
    Console.WriteLine($"Szczegóły: {ex.Message}");
    return 1;
}

Console.WriteLine("Wszystkie faktury:");
foreach (Invoice invoice in invoices)
{
    PrintInvoice(invoice);
}

decimal totalGross = invoices.Sum(invoice => invoice.GrossAmount);
Console.WriteLine();
Console.WriteLine($"Suma brutto: {totalGross:N2}");

DateOnly today = DateOnly.FromDateTime(DateTime.Today);
List<Invoice> overdueInvoices = invoices
    .Where(invoice => invoice.DueDate < today)
    .ToList();

Console.WriteLine();
Console.WriteLine($"Faktury przeterminowane (stan na {today:yyyy-MM-dd}):");
if (overdueInvoices.Count == 0)
{
    Console.WriteLine("  brak");
}
foreach (Invoice invoice in overdueInvoices)
{
    PrintInvoice(invoice);
}

return 0;

static void PrintInvoice(Invoice invoice)
{
    Console.WriteLine(
        $"  {invoice.InvoiceNumber,-16} {invoice.ContractorName,-32} " +
        $"{invoice.GrossAmount,12:N2} {invoice.Currency}  termin: {invoice.DueDate:yyyy-MM-dd}");
}
