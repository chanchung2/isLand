using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : Singleton<TileMapManager>
{
    [SerializeField]
    private Tilemap m_SoilTileMap;
    [SerializeField]
    private Tilemap m_ObjectTileMap;

    // 흙 타일 지정.

    public void SetSoilTile(Vector3 pos)
    {
        var terrainTile = Resources.Load<TerrainTile>("Tile/Soil/SoilTile");

        Vector3Int tilePos = m_SoilTileMap.WorldToCell(pos);
        m_SoilTileMap.SetTile(tilePos, terrainTile);

    }

    public Vector3Int GetWorldToCell(Vector3 playerPos)
    {
        return m_ObjectTileMap.WorldToCell(playerPos);
    }
}
