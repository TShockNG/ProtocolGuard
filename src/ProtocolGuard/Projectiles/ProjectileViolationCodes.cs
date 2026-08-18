namespace TShockNG.ProtocolGuard.Projectiles;

/// <summary>
/// Stable violation codes returned by projectile validators.
/// </summary>
public static class ProjectileViolationCodes
{
    /// <summary>
    /// Projectile position contains NaN or infinity.
    /// </summary>
    public const string NonFinitePosition = "projectile.position.non_finite";

    /// <summary>
    /// Projectile velocity contains NaN or infinity.
    /// </summary>
    public const string NonFiniteVelocity = "projectile.velocity.non_finite";

    /// <summary>
    /// Projectile damage exceeds the configured server limit.
    /// </summary>
    public const string MaximumDamageExceeded = "projectile.damage.maximum_exceeded";

    /// <summary>
    /// Projectile AI0 is outside the range accepted for this projectile type.
    /// </summary>
    public const string AI0OutOfRange = "projectile.ai0.out_of_range";

    /// <summary>
    /// Projectile AI1 is outside the range accepted for this projectile type.
    /// </summary>
    public const string AI1OutOfRange = "projectile.ai1.out_of_range";
}
