namespace DocumentFlow.Console.Models;

public class Invoice
{
    public required string InvoiceNumber { get; init; }
    public required string ContractorName { get; init; }
    public required string ContractorNip { get; init; }
    public required DateOnly IssueDate { get; init; }
    public required DateOnly DueDate { get; init; }
    public required decimal NetAmount { get; init; }
    public required decimal VatAmount { get; init; }
    public required decimal GrossAmount { get; init; }
    public required string Currency { get; init; }
}
