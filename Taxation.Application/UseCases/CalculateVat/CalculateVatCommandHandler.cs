using FluentResults;
using MediatR;
using Taxation.Domain.Models;
using Taxation.Domain.Services;
using Taxation.Domain.ValueObjects;

namespace Taxation.Application.UseCases.CalculateVat;

public sealed class CalculateVatCommandHandler(IVatCalculator calculator)
    : IRequestHandler<CalculateVatCommand, Result<CalculateVatResult>>
{
    public Task<Result<CalculateVatResult>> Handle(CalculateVatCommand request, CancellationToken cancellationToken)
    {
        var vatRatePercent = new Percentage(request.VatRatePercent / 100m);

        var inputAmount = request.Vat.HasValue
            ? new InputAmount(request.Vat.Value, AmountKind.Vat)
            : request.Gross.HasValue
                ? new InputAmount(request.Gross.Value, AmountKind.Gross)
                : new InputAmount(request.Net!.Value, AmountKind.Net);

        var result = calculator.Calculate(inputAmount, vatRatePercent);

        return Task.FromResult(
            Result.Ok(new CalculateVatResult(result.Net, result.Vat, result.Gross, request.VatRatePercent)));
    }
}