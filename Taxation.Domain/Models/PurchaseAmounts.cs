namespace Taxation.Domain.Models;

public sealed record PurchaseAmounts(decimal Net, decimal Gross, decimal Vat);