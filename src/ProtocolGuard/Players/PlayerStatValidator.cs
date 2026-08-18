using Multiplicity.Packets.Views;

namespace TShockNG.ProtocolGuard.Players;

/// <summary>
/// Validates player HP and mana synchronization values without applying server-side effects.
/// </summary>
public static class PlayerStatValidator
{
    /// <summary>
    /// Validates a player HP synchronization packet using TShock-compatible limit semantics.
    /// </summary>
    /// <param name="packet">Typed HP packet view.</param>
    /// <param name="policy">Configured maximum and exemption state.</param>
    /// <returns>A structured validation result.</returns>
    public static ValidationResult Validate(PlayerHpView packet, PlayerStatValidationPolicy policy)
    {
        if (packet.Hp <= 0)
        {
            return ValidationResult.Reject(new Violation(
                PlayerStatViolationCodes.NonPositiveHealth,
                ViolationCategory.OutOfRange));
        }

        if (packet.MaxHp <= 0)
        {
            return ValidationResult.Reject(new Violation(
                PlayerStatViolationCodes.NonPositiveMaximumHealth,
                ViolationCategory.OutOfRange));
        }

        if (!policy.IgnoreMaximum && packet.MaxHp > policy.Maximum)
        {
            return ValidationResult.Reject(new Violation(
                PlayerStatViolationCodes.MaximumHealthExceeded,
                ViolationCategory.Policy,
                $"received={packet.MaxHp}; allowed={policy.Maximum}"));
        }

        return ValidationResult.Allow();
    }

    /// <summary>
    /// Validates a player mana synchronization packet using TShock-compatible limit semantics.
    /// </summary>
    /// <param name="packet">Typed mana packet view.</param>
    /// <param name="policy">Configured maximum and exemption state.</param>
    /// <returns>A structured validation result.</returns>
    public static ValidationResult Validate(PlayerManaView packet, PlayerStatValidationPolicy policy)
    {
        if (packet.Mana < 0)
        {
            return ValidationResult.Reject(new Violation(
                PlayerStatViolationCodes.NegativeMana,
                ViolationCategory.OutOfRange));
        }

        if (packet.MaxMana < 0)
        {
            return ValidationResult.Reject(new Violation(
                PlayerStatViolationCodes.NegativeMaximumMana,
                ViolationCategory.OutOfRange));
        }

        if (!policy.IgnoreMaximum && packet.MaxMana > policy.Maximum)
        {
            return ValidationResult.Reject(new Violation(
                PlayerStatViolationCodes.MaximumManaExceeded,
                ViolationCategory.Policy,
                $"received={packet.MaxMana}; allowed={policy.Maximum}"));
        }

        return ValidationResult.Allow();
    }
}
