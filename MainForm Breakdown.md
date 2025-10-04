# MainForm Breakdown

## Core shell (`MainForm.cs`)
- Maintains a static `Instance`, lazily populated control references, binding sources for classes, units, spawn zones, and datapack path metadata shared across the application.


- The constructor seeds the UI by creating the default item image, running designer initialization, attaching a console, and delegating to the item, class, loot, race, and spawn-zone setup routines.


## Class editor (`MainForm.cs`)
- `InitializeClassTab` builds the “Classes” tab layout, wiring list selection handlers and CRUD buttons for class records.


- Supporting methods bind `ClassData` instances, keep labels in sync with the selection, and implement add/update/delete flows with validation and stateful button toggling.


## Datapack management (`MainForm.cs`)
- Menu handlers prompt for archive vs. folder sources, load item/tile data through the managers, persist datapacks to disk or zip archives, and clean up temporary extraction directories with defensive error handling.


## Spawn zones & assignments (`MainForm.cs`)
- `InitializeSpawnZoneTab` composes the unit list, spawn-zone editor, and assignment panes with their event hooks and numeric controls.


- Binding logic manages unit and zone collections, propagates edits into assignments, enforces range validation, and detaches or reuses assignments when records change.


## Race editor (`RaceForm.cs`)
- The race partial builds its own tab, binds a `RaceData` list, and supports creating, updating, and deleting races while guarding against missing input and reflecting the current selection in the UI.


## Item authoring (`ItemForm.cs`)
- Defines list rendering constants, caches, and `FoldDataIntoItem` to construct the appropriate `ItemData` subtype with validation across bag, consumable, equipment, and weapon variants.


- Initialization primes type selectors, while handlers preview tooltips, create or override items, and populate fields when editing existing entries.


- Change-tracking helpers compare form fields against the original data, prompt before discarding edits, and toggle weapon-specific UI based on the selected type.


## Item imagery helpers (`MainForm.ItemImages.cs`)
- Generates a default placeholder sprite, owner-draws list entries with quality-based colors, caches loaded bitmaps, scans datapack/app directories for textures, and disposes cached images when shutting down.


## Tile editor (`TileForm.cs`)
- Translates the tile editor form into `TileData`, enforces unique IDs, tracks the editing slot, and exposes create/update/reset actions backed by the `TileManager` API.


## Loot tables (`LootForm.cs`)
- Wires the loot tab controls, configures the entry grid, toggles form state based on selection, and delegates table/entry CRUD to `LootManager` with binding refreshes.


- Computes and renders binomial drop distributions per entry, adjusting axis scaling to reflect the selected kill count.


## Lifecycle & auxiliary commands
- The designer `Dispose` override releases cached images, and `OnFormClosed` deletes any lingering datapack extraction folders before closing.


- Menu handlers provide chat download instructions and open the sprite editor window, illustrating auxiliary tooling beyond the main tabs.


## Testing
⚠️ Tests not run (not requested).
