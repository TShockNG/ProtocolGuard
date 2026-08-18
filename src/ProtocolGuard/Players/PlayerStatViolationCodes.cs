namespace TShockNG.ProtocolGuard.Players;

/// <summary>
/// Stable violation codes returned by player stat validators.
/// </summary>
public static class PlayerStatViolationCodes
{
    /// <summary>
    /// The synchronized current HP value is non-positive.
    /// </summary>
    public const string NonPositiveHealth = "player.hp.non_positive_current";

    /// <summary>
    /// The synchronized maximum HP value is non-positive.
    /// </summary>
    public const string NonPositiveMaximumHealth = "player.hp.non_positive_maximum";

    /// <summary>
    /// The synchronized maximum HP exceeds the configured server limit.
    /// </summary>
    public const string MaximumHealthExceeded = "player.hp.maximum_exceeded";

    /// <summary>
    /// The synchronized current mana value is negative.
    /// </summary>
    public const string NegativeMana = "player.mana.negative_current";

    /// <summary>
    /// The synchronized maximum mana value is negative.
    /// </summary>
    public const string NegativeMaximumMana = "player.mana.negative_maximum";

    /// <summary>
    /// The synchronized maximum mana exceeds the configured server limit.
    /// </summary>
    public const string MaximumManaExceeded = "player.mana.maximum_exceeded";
}
