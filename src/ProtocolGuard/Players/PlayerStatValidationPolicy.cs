namespace TShockNG.ProtocolGuard.Players;

/// <summary>
/// Defines server-side limits used when validating a player stat synchronization packet.
/// </summary>
/// <param name="Maximum">Maximum allowed value.</param>
/// <param name="IgnoreMaximum">Whether the sender is exempt from the configured maximum.</param>
public readonly record struct PlayerStatValidationPolicy(int Maximum, bool IgnoreMaximum = false);
