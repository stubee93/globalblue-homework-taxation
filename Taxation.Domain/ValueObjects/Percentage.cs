using Taxation.Domain.Exceptions;

namespace Taxation.Domain.ValueObjects;

public readonly record struct Percentage
{
    public decimal Value { get; }
    
    public Percentage(decimal value)
    {
        if (value is < 0 or > 1)
        {
            throw new DomainException("Percentage must be between 0 and 1.");
        }
        
        Value = value;
    }
}