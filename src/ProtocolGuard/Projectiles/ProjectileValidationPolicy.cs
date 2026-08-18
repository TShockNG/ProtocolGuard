namespace TShockNG.ProtocolGuard.Projectiles;

/// <summary>
/// Defines server-side policy used when validating a projectile synchronization packet.
/// </summary>
/// <param name="MaximumDamage">Maximum allowed projectile damage.</param>
/// <param name="IgnoreMaximumDamage">Whether the sender is exempt from the configured damage maximum.</param>
public readonly record struct ProjectileValidationPolicy(int MaximumDamage, bool IgnoreMaximumDamage = false);
