# ProtocolGuard roadmap

## Phase 0 — foundations

- [x] Create .NET 9 project structure.
- [x] Reference `TZ.Multiplicity` through NuGet.
- [x] Define `ValidationDecision`.
- [x] Define allocation-conscious `ValidationResult`.
- [x] Define the initial violation code/category model.
- [ ] Add unit tests for migrated validators.
- [x] Establish cross-platform build CI.

## Phase 1 — first pure validators

- [ ] Projectile validation using Multiplicity typed packet data.
- [ ] Item validation.
- [ ] Player stat/state validation.
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
