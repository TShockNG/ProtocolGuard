# ProtocolGuard roadmap

## Phase 0 — foundations

- [x] Create .NET 9 project structure.
- [x] Reference the currently published `TZ.Multiplicity` package through NuGet.
- [x] Define `ValidationDecision`.
- [x] Define allocation-conscious `ValidationResult`.
- [x] Define the initial violation code/category model.
- [x] Add unit tests for the first migrated validators.
- [x] Establish cross-platform build/test CI.
- [x] Upgrade `TZ.Multiplicity` to `2.6.0` for Terraria protocol 319.

## Phase 1 — first pure validators

- [x] Extract packet-only projectile validation from Bouncer.
- [ ] Item validation.
- [x] Player HP/mana stat validation.
- [ ] Additional player state validation.
- [ ] Coordinate/range primitives shared by world-interaction validators.

## Phase 2 — world interaction

- [ ] Tile edit validation.
- [ ] Object placement validation.
- [ ] Chest/container validation.

## Phase 3 — integration quality

- [ ] Benchmarks for hot validators.
- [ ] Fuzz/property tests for malformed packet/action data.
- [ ] Document migrated TShock behavior and compatibility differences.
- [ ] Ensure validators have no TShock/TSAPI/Terraria global-state dependency.
