# Migration from TShock Bouncer and handlers

## Extraction rule

Move logic only after side effects and mutable state access have been separated from the pure validation decision.

Bad target shape:

```csharp
if (invalid)
{
    player.Disable(...);
    player.SendErrorMessage(...);
    NetMessage.SendData(...);
}
```

Preferred target shape:

```csharp
ValidationResult result = validator.Validate(action, context);
```

TShockNG then decides how to enforce the result.

## Extraction order

Start with validators that have:

- simple packet inputs;
- limited world context;
- clear existing behavior;
- good regression-test potential.

Suggested order:

1. projectile ownership/sanity;
2. item sanity;
3. player stat/state sanity;
4. range checks;
5. tile/object placement validation;
6. chest/container validation;
7. complex world/NPC rules.

## Snapshot design

Snapshots should contain only fields needed by the validator. Do not create a giant mirror of all Terraria state merely to make the API look generic.
