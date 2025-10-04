# Datapack Overview

## Tile data schema

Tile metadata now uses the richer schema introduced in
`docs/datapack/Tiles/000*_*.json` files:

```json
{
  "ID": 0,
  "Name": "Example",
  "Texture": "tile_example",
  "IsWalkable": true,
  "MovementCost": 1,
  "Notes": "Flavor text shown to designers."
}
```

## Canonical representation

`docs/datapack/TileData.json` is the aggregated form consumed by tools.
It contains a JSON array ordered by `ID`. The file replaces the legacy
newline-delimited representation and should remain valid JSON.

The individual files in `docs/datapack/Tiles/` remain as the authoring
source for designers because they are easier to edit and review
individually. Both representations therefore continue to exist: the
per-tile files are edited by humans, and the aggregated array is
regenerated for downstream consumption.

## Regenerating `TileData.json`

Use `tools/aggregate_tile_data.py` to rebuild the aggregate file whenever
per-tile JSON definitions are updated or new tiles are added. The script
collects the individual definitions, validates that they contain the
required fields, and writes the combined array back to `TileData.json`.

## Debug datapack directory

When running the WinForms tool from source, the application looks for a
datapack directory during debug builds so file pickers start in a useful
location. By default it resolves to the repository's
`docs/datapack` folder. Developers can override this without touching the
code by setting the `SPHMMaker_DebugDatapackDirectory` environment
variable to point at an alternate directory on their machine. The path is
validated during startup to catch typos early.
