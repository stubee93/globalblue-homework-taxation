namespace Taxation.Api.Contracts;

public record CalculateVatRequest(decimal? Net,
    decimal? Gross,
    decimal? Vat,
    int VatRate);