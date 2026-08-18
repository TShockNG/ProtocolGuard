# ProtocolGuard

ProtocolGuard is the reusable protocol-validation layer for TShockNG.

It validates typed client actions against immutable context and returns structured decisions. It does **not** kick players, send packets, mutate Terraria state, write to databases, or depend on global TShock state.

The project is intentionally kept independent from TShockNG domain code so validation rules can be tested in isolation and reused by other server components.

See [`docs/`](docs/) for architecture, migration rules, and the roadmap.
