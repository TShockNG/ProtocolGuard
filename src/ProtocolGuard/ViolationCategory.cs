namespace TShockNG.ProtocolGuard;

/// <summary>
/// Broad, stable categories used to classify protocol violations.
/// </summary>
public enum ViolationCategory : byte
{
    /// <summary>
    /// The action is structurally or semantically malformed.
    /// </summary>
    Malformed = 0,

    /// <summary>
    /// One or more values are outside their valid range.
    /// </summary>
    OutOfRange = 1,

    /// <summary>
    /// The client attempted to act on state it does not own.
    /// </summary>
    Ownership = 2,

    /// <summary>
    /// The action conflicts with the supplied server/player state snapshot.
    /// </summary>
    State = 3,

    /// <summary>
    /// The action violates a configured validation policy.
    /// </summary>
    Policy = 4,

    /// <summary>
    /// The violation does not yet belong to a more specific category.
    /// </summary>
    Unknown = 255
}
