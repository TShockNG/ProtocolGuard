namespace TShockNG.ProtocolGuard;

/// <summary>
/// Describes a machine-readable protocol validation violation.
/// </summary>
/// <param name="Code">Stable rule-specific code suitable for diagnostics and tests.</param>
/// <param name="Category">Broad violation category.</param>
/// <param name="Detail">Optional diagnostic detail. This is not user-facing localized text.</param>
public readonly record struct Violation(
    string Code,
    ViolationCategory Category,
    string? Detail = null);
