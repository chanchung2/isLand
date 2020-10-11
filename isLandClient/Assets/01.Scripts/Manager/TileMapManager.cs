using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : Singleton<TileMapManager>
{
    [SerializeField]
    private Tilemap m_SoilTileMap;

    // 흙 타일 지정.

    public void SetSoilTile(Vector3 playerPos)
    {
        var terrainTile = Resources.Load<TerrainTile>("Tile/Soil/SoilTile");

        Vector3Int tilePos = m_SoilTileMap.WorldToCell(playerPos);
        m_SoilTileMap.SetTile(tilePos, terrainTile);
    }
}
