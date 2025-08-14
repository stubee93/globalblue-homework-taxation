using Taxation.Domain.ValueObjects;

namespace Taxation.Domain.Models;

public sealed record InputAmount(decimal Amount, AmountKind Kind);