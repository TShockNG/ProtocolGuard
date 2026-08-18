# ProtocolGuard roadmap

## Phase 0 — foundations

- [x] Create .NET 9 project structure.
- [x] Reference the currently published `TZ.Multiplicity` package through NuGet.
- [x] Define `ValidationDecision`.
- [x] Define allocation-conscious `ValidationResult`.
- [x] Define the initial violation code/category model.
- [x] Add unit tests for the first migrated validators.
- [x] Establish cross-platform build/test CI.
- [ ] Upgrade `TZ.Multiplicity` from `2.5.1` to the protocol-319 package when that package is published.

## Phase 1 — first pure validators

- [ ] Projectile validation using Multiplicity typed packet data.
- [ ] Item validation.
- [x] Player HP/mana stat validation.
- [ ] Additional player state validation.
- [ ] Coordinate/range primitives.

## Phase 2 — world interaction

- [ ] Tile edit validation.
- [ ] Object placement validation.
- [ ] Chest/container validation.

## Phase 3 — integration quality

- [ ] Benchmarks for hot validators.
- [ ] Fuzz/property tests for malformed packet/action data.
- [ ] Document migrated TShock behavior and compatibility differences.
- [ ] Ensure validators have no TShock/TSAPI/Terraria global-state dependency.
