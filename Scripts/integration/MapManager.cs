using System;
using Godot;
using Godot.Collections;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class MapManager : TileMap
{
    Dictionary<Vector2, int> _tileHealth = new Dictionary<Vector2, int>();

    public void InitializeTileMap(TileMap tileMap)
    {
        foreach (Vector2I pos in tileMap.GetUsedCells(0))
        {
            var tileData = tileMap.GetCellTileData(0, pos);
            var hits = (int)tileData.GetCustomDataByLayerId(0);
            _tileHealth[pos] = hits;
        }
    }

    public override void _Ready()
    {
        InitializeTileMap(this);
    }
}