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
            
        }
    }

}