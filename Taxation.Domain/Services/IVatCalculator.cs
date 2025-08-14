using Taxation.Domain.Models;
using Taxation.Domain.ValueObjects;

namespace Taxation.Domain.Services;

public interface IVatCalculator
{
    PurchaseAmounts Calculate(InputAmount input, Percentage vatRate);
}