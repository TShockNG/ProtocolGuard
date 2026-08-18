using Multiplicity.Packets.Views;

namespace TShockNG.ProtocolGuard.Projectiles;

/// <summary>
/// Performs side-effect-free validation of projectile synchronization data.
/// </summary>
public static class ProjectileValidator
{
    /// <summary>
    /// Validates packet-only projectile constraints that do not require live Terraria state.
    /// </summary>
    /// <param name="packet">Typed projectile packet view.</param>
    /// <param name="policy">Configured projectile policy and exemptions.</param>
    /// <returns>A structured validation result.</returns>
    public static ValidationResult Validate(ProjectileNewView packet, ProjectileValidationPolicy policy)
    {
        if (!float.IsFinite(packet.PositionX) || !float.IsFinite(packet.PositionY))
        {
            return ValidationResult.Reject(new Violation(
                ProjectileViolationCodes.NonFinitePosition,
                ViolationCategory.Malformed));
        }

        if (!float.IsFinite(packet.VelocityX) || !float.IsFinite(packet.VelocityY))
        {
            return ValidationResult.Reject(new Violation(
                ProjectileViolationCodes.NonFiniteVelocity,
                ViolationCategory.Malformed));
        }

        if (!policy.IgnoreMaximumDamage && packet.Damage > policy.MaximumDamage)
        {
            return ValidationResult.Reject(new Violation(
                ProjectileViolationCodes.MaximumDamageExceeded,
                ViolationCategory.Policy,
                $"received={packet.Damage}; allowed={policy.MaximumDamage}"));
        }

        if (TryGetAI0Range(packet.Type, out float ai0Minimum, out float ai0Maximum) &&
            (packet.AI0 < ai0Minimum || packet.AI0 > ai0Maximum))
        {
            return ValidationResult.Reject(new Violation(
                ProjectileViolationCodes.AI0OutOfRange,
                ViolationCategory.OutOfRange,
                $"type={packet.Type}; received={packet.AI0}; allowed=[{ai0Minimum},{ai0Maximum}]"));
        }

        if (TryGetAI1Range(packet.Type, out float ai1Minimum, out float ai1Maximum) &&
            (packet.AI1 < ai1Minimum || packet.AI1 > ai1Maximum))
        {
            return ValidationResult.Reject(new Violation(
                ProjectileViolationCodes.AI1OutOfRange,
                ViolationCategory.OutOfRange,
                $"type={packet.Type}; received={packet.AI1}; allowed=[{ai1Minimum},{ai1Maximum}]"));
        }

        return ValidationResult.Allow();
    }

    private static bool TryGetAI0Range(short projectileType, out float minimum, out float maximum)
    {
        switch (projectileType)
        {
            case 611:
                minimum = -1f;
                maximum = 1f;
                return true;
            case 950:
                minimum = 0f;
                maximum = 0f;
                return true;
            case 502:
                minimum = 0f;
                maximum = 5f;
                return true;
            default:
                minimum = 0f;
                maximum = 0f;
                return false;
        }
    }

    private static bool TryGetAI1Range(short projectileType, out float minimum, out float maximum)
    {
        switch (projectileType)
        {
            case 405:
            case 410:
                minimum = 0f;
                maximum = 1.2f;
                return true;
            case 424:
            case 425:
            case 426:
                minimum = 0.5f;
                maximum = 0.8f;
                return true;
            case 612:
                minimum = 0.4f;
                maximum = 0.7f;
                return true;
            case 953:
                minimum = 0.85f;
                maximum = 2f;
                return true;
            case 756:
                minimum = 0.5f;
                maximum = 1f;
                return true;
            case 522:
                minimum = 0f;
                maximum = 40f;
                return true;
            case 459:
                minimum = 0.7f;
                maximum = 1.3f;
                return true;
            default:
                minimum = 0f;
                maximum = 0f;
                return false;
        }
    }
}
