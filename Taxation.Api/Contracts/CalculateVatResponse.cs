namespace Taxation.Api.Contracts;

public record CalculateVatResponse(
    decimal Net,
    decimal Gross,
    decimal Vat,
    int VatRate);