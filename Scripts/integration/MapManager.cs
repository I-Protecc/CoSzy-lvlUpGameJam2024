using System;
using Godot;
using Godot.Collections;

namespace GameJamPlaceHolderName.Scripts.integration;

public partial class MapManager : TileMap
{
    Dictionary<Vector2, int> _tileHealth = new Dictionary<Vector2, int>();

    private void InitializeTileMap(TileMap tileMap)
    {
        foreach (Vector2I pos in tileMap.GetUsedCells(0))
        {
            var tileData = tileMap.GetCellTileData(0, pos);
            var hits = (int)tileData.GetCustomDataByLayerId(0);
            _tileHealth[pos] = hits;
            GD.Print(pos);
        }
    }

    public void MineTile(Vector2I tilePos)
    {
        if (_tileHealth.ContainsKey(tilePos))
        {
            _tileHealth[tilePos] -= 1;
        }
        else
            return;
        
        if(_tileHealth[tilePos] <= 0)
            SetCell(0, tilePos);
    }

    public override void _Ready()
    {
        InitializeTileMap(this);
    }
}