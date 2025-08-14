using FluentResults;
using MediatR;

namespace Taxation.Application.UseCases.CalculateVat;

public record CalculateVatCommand(decimal? Net, decimal? Gross, decimal? Vat, int VatRatePercent, string CountryCode) 
    : IRequest<Result<CalculateVatResult>>;