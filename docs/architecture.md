# ProtocolGuard architecture

## Purpose

ProtocolGuard answers one question:

> Is this client action semantically acceptable under the supplied validation context?

It does not decide how TShockNG should punish, correct, message, log, or disconnect a player.

## Inputs

A validator receives:

1. typed packet/action data;
2. immutable validation context/snapshot;
3. validation policy/options where required.

ProtocolGuard must not depend on `TSPlayer`, `TShock`, `Terraria.Main`, or other mutable global server state.

## Result

The common result model has three decisions:

- `Allowed`
- `Rejected`
- `Corrected`

Rejected/corrected results carry a structured `Violation` with a stable code/category and optional diagnostic detail. User-facing text does not belong in the validation library.

Example conceptual model:

```csharp
public readonly record struct ValidationResult(
    ValidationDecision Decision,
    Violation? Violation = null);
```

The exact API should stay allocation-conscious on hot paths.

## Side-effect boundary

ProtocolGuard must not:

- kick or disable a player;
- send network packets;
- write chat messages;
- mutate Terraria state;
- modify databases;
- access global TShock singletons.

TShockNG consumes the result and performs enforcement.

## Performance

Validators on hot packet paths should be synchronous and allocation-light. Background execution is reserved for genuinely expensive checks that can operate on immutable snapshots.
