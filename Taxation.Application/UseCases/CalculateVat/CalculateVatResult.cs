namespace Taxation.Application.UseCases.CalculateVat;

public record CalculateVatResult(decimal Net, decimal Vat, decimal Gross, int VatRatePercent);