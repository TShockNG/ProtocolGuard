namespace TShockNG.ProtocolGuard;

/// <summary>
/// Describes how the caller should treat a validated client action.
/// </summary>
public enum ValidationDecision : byte
{
    /// <summary>
    /// The action passed validation unchanged.
    /// </summary>
    Allowed = 0,

    /// <summary>
    /// The action must not be applied.
    /// </summary>
    Rejected = 1,

    /// <summary>
    /// The original action must not be applied, but the caller may apply a corrected state.
    /// </summary>
    Corrected = 2
}
