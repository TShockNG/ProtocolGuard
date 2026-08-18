# 0002: Validation contexts are minimal immutable snapshots

## Status

Accepted

## Decision

Each validator receives only the immutable state required for that validation. There is no universal mutable server context object.

## Consequences

- Validators remain explicit about dependencies.
- Background-safe validation becomes possible where useful.
- Snapshot copying stays bounded to data actually needed by a rule.
