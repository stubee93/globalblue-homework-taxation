using Taxation.Domain.Exceptions;
using Taxation.Domain.Models;
using Taxation.Domain.ValueObjects;

namespace Taxation.Domain.Services;

public class VatCalculator : IVatCalculator
{
    // TODO: Rounding would be nice
    // TODO: Unit tests would be also nice
    public PurchaseAmounts Calculate(InputAmount input, Percentage vatRate)
    {
        return input.Kind switch
        {
            AmountKind.Net  => FromNet (input.Amount, vatRate),
            AmountKind.Gross=> FromGross(input.Amount, vatRate),
            AmountKind.Vat  => FromVat (input.Amount, vatRate),
            _ => throw new DomainException("Unsupported amount kind.")
        };
    }

    private static PurchaseAmounts FromNet(decimal net, Percentage vatRate)
    {
        var vat = net * vatRate.Value;
        var gross = net + vat;
        return new PurchaseAmounts(net, gross, vat);
    }
    
    private static PurchaseAmounts FromGross(decimal gross, Percentage vatRate)
    {
        var net = gross / (1 + vatRate.Value);
        var vat = gross - net;
        return new PurchaseAmounts(net, vat, gross);
    }

    private static PurchaseAmounts FromVat(decimal vat, Percentage vatRate)
    {
        var net   = vat / vatRate.Value;
        var gross = net + vat;
        return new PurchaseAmounts(net, vat,gross);
    }
}