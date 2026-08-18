# 0001: ProtocolGuard has no server side effects

## Status

Accepted

## Decision

ProtocolGuard performs validation and returns structured results. It does not kick players, send packets, log to TShock, mutate Terraria state, or access persistent storage.

## Consequences

- Validators are deterministic and unit-testable.
- TShockNG owns enforcement policy.
- The library can be reused outside TShockNG if needed.
