namespace TShockNG.ProtocolGuard;

/// <summary>
/// Represents the side-effect-free result of validating a client action.
/// </summary>
public readonly record struct ValidationResult
{
    private ValidationResult(ValidationDecision decision, Violation? violation)
    {
        Decision = decision;
        Violation = violation;
    }

    /// <summary>
    /// Gets the validation decision.
    /// </summary>
    public ValidationDecision Decision { get; }

    /// <summary>
    /// Gets violation metadata when the action was rejected or corrected.
    /// </summary>
    public Violation? Violation { get; }

    /// <summary>
    /// Gets whether the action passed validation unchanged.
    /// </summary>
    public bool IsAllowed => Decision == ValidationDecision.Allowed;

    /// <summary>
    /// Creates an allowed result.
    /// </summary>
    public static ValidationResult Allow() => new(ValidationDecision.Allowed, null);

    /// <summary>
    /// Creates a rejected result.
    /// </summary>
    /// <param name="violation">The violation that caused rejection.</param>
    public static ValidationResult Reject(Violation violation) =>
        new(ValidationDecision.Rejected, violation);

    /// <summary>
    /// Creates a corrected result.
    /// </summary>
    /// <param name="violation">The violation that requires correction.</param>
    public static ValidationResult Correct(Violation violation) =>
        new(ValidationDecision.Corrected, violation);
}
