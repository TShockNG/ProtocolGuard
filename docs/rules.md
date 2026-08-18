# ProtocolGuard rule taxonomy

This file records rule ownership as validation is extracted from TShock.

## Player

Implemented:

- HP synchronization: reject non-positive current/max HP and configured maximum HP violations.
- Mana synchronization: reject negative current/max mana and configured maximum mana violations.

The initial rules deliberately preserve current TShock semantics. In particular, the HP rule treats zero HP as invalid while the mana rule permits zero mana.

Candidates:

- movement/state sanity
- packet player-index ownership checks
- PvP/team/zones state sanity
- inventory/equipment state sanity
- interaction range checks

## Projectile

Implemented from the packet-only portion of the existing TShock Bouncer logic:

- reject non-finite position values;
- reject non-finite velocity values;
- reject damage above the configured maximum unless the caller supplies an exemption;
- preserve the existing projectile-type-specific AI0/AI1 ranges.

The following Bouncer behavior deliberately remains in TShockNG because it requires live server state or enforcement policy:

- projectile bans and player permissions;
- held-item/direction checks;
- projectile count/rate thresholds;
- active projectile/world lookups;
- recent projectile/fuse tracking;
- hostile-projectile tables;
- portal-gun active bolt state;
- kick/disable/correction/network side effects.

## Item

Candidates:

- item ID/prefix/stack validity
- ownership/reservation constraints
- impossible remote creation/drop conditions

## Tile and world interaction

Candidates:

- coordinate bounds
- placement style validity
- range checks
- tile edit action sanity
- object placement sanity

## Chest/container

Candidates:

- chest index/coordinate consistency
- item slot validity
- interaction range/ownership state

## NPC and entities

Candidates:

- NPC strike/action sanity
- buff target validity
- release/catch/action range checks

## Rule documentation requirement

Each migrated rule should record:

- source TShock handler/Bouncer location;
- required input fields;
- required snapshot/context fields;
- validation result codes;
- regression tests, including known exploit/malformed cases where available.
