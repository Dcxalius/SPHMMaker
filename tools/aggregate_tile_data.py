#!/usr/bin/env python3
"""Aggregate tile definitions into a single JSON array.

This script reads every JSON file inside ``docs/datapack/Tiles`` whose
filename begins with a numeric tile identifier (``000*_``) and maps the
fields into the canonical tile schema used by ``docs/datapack/TileData.json``.
"""

from __future__ import annotations

import json
from dataclasses import dataclass
from pathlib import Path
from typing import List

ROOT = Path(__file__).resolve().parents[1]
TILES_DIR = ROOT / "docs" / "datapack" / "Tiles"
OUTPUT_FILE = ROOT / "docs" / "datapack" / "TileData.json"


@dataclass(order=True)
class TileRecord:
    """Canonical representation of a tile."""

    ID: int
    Name: str
    Texture: str
    IsWalkable: bool
    MovementCost: int
    Notes: str

    @classmethod
    def from_json(cls, payload: dict) -> "TileRecord":
        """Create a tile record from a raw JSON mapping."""

        required_fields = {
            "ID": int,
            "Name": str,
            "Texture": str,
            "IsWalkable": bool,
            "MovementCost": int,
            "Notes": str,
        }

        missing = [key for key in required_fields if key not in payload]
        if missing:
            raise ValueError(f"Tile record missing required fields: {missing}")

        # Perform basic type conversion/validation.
        kwargs = {}
        for key, expected_type in required_fields.items():
            value = payload[key]
            if not isinstance(value, expected_type):
                raise TypeError(
                    f"Field '{key}' expected {expected_type.__name__}, got {type(value).__name__}"
                )
            kwargs[key] = value
        return cls(**kwargs)


def load_tiles() -> List[TileRecord]:
    tiles: List[TileRecord] = []
    for tile_path in sorted(TILES_DIR.glob("000*_*.json")):
        with tile_path.open("r", encoding="utf-8") as handle:
            payload = json.load(handle)
        tiles.append(TileRecord.from_json(payload))
    tiles.sort(key=lambda tile: tile.ID)
    return tiles


def write_output(tiles: List[TileRecord]) -> None:
    data = [tile.__dict__ for tile in tiles]
    with OUTPUT_FILE.open("w", encoding="utf-8") as handle:
        json.dump(data, handle, indent=2)
        handle.write("\n")


def main() -> None:
    tiles = load_tiles()
    write_output(tiles)


if __name__ == "__main__":
    main()
