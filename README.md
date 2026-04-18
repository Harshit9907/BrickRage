# Brick Rage (Unity URP MVP)

This repository now contains a production-ready **MVP gameplay foundation** for the Brick Rage hero shooter concept in Unity (landscape, left-anchored hero, right-side battlefield, ricochet volley combat).

## What is implemented

- Core gameplay config via `ScriptableObject` (`GameConfig`) to tune feel quickly.
- Drag-to-aim + release-to-fire volley controller with trajectory preview and wall reflection preview.
- Projectile bounce physics with per-bounce momentum decay.
- Damageable bricks with HP labels and shatter hooks.
- Advancing enemy formation + breach-line pressure against hero HP.
- Hero health and destruction meter systems.
- Level controller with basic win/loss loop and rage-burst screen clear behavior.
- HUD controller wiring for HP and Rage UI.
- Ad placement policy scaffolding aligned with the production bible rules.

## Unity setup (day-1 playable)

1. Open Unity Hub and create/open a **Unity 6 / 2022+ URP** project.
2. Copy this repo contents into the project root (or clone directly as project root).
3. In `Project Settings > Tags and Layers`, add a `Walls` layer.
4. Create a `GameConfig` asset:
   - Right click in Project -> `Create > BrickRage > Game Config`.
5. Create a gameplay scene and wire references:
   - `AimAndShootController` on hero root.
   - `Projectile` prefab with `Rigidbody2D` (Dynamic, no gravity) + `CircleCollider2D`.
   - Top and bottom boundaries as colliders on `Walls` layer.
   - `Brick` objects with collider + optional TextMeshPro label.
   - `FormationController`, `HeroHealth`, `DestructionMeter`, `LevelController`, `BrickRageLinker`, `HudController`.
6. Press Play and iterate values in `GameConfig` until the feel matches spec.

## Deployment checklist (next sprint)

Before store deployment, complete:

- Real level-data pipeline for 1-100 (ScriptableObjects/Addressables).
- Boss framework and all advanced brick mechanics.
- Full UI flow (Home, Level Select, Game Over, Reward, Leaderboards).
- Ad SDK integration (AdMob mediation + network adapters).
- Save/progression backend and rival system data source.
- Device profiling + asset compression + crash/error analytics.

This codebase is structured to unblock gameplay prototyping immediately and evolve into full production implementation.
