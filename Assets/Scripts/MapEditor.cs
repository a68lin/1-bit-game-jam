using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Normalmap = System.Collections.Generic.List<System.Collections.Generic.List<int>>;

public class MapEditor : MonoBehaviour
{
    private class MapModel
    {
        private Normalmap normalMap;
        private Vector3 offset;
    }

    public Tilemap[] baseTilemaps;
    public TileBase[] baseTiles;

    public Vector3 defaultOffset;

    private MapModel[] Maps;

    private Tilemap dummyTilemapPrefab;

    private void Awake()
    {
        dummyTilemapPrefab = baseTilemaps[0];
    }

    private void Start()
    {
        // [TODO]
        Normalmap normalmap = TilemapToNormalmap(baseTilemaps[0]);

        Tilemap testCopy1 = NormalmapToTilemap(normalmap, baseTiles[0]);
        testCopy1.transform.parent = transform;
        testCopy1.name = "Test";
    }

    private Normalmap TilemapToNormalmap(Tilemap tilemap)
    {
        Normalmap normalmap = new Normalmap();

        tilemap.CompressBounds();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        int rows = bounds.size.y;
        int cols = bounds.size.x;

        for (int r = 0; r < rows; ++r)
        {
            List<int> tmpList = new List<int>();
            for (int c = 0; c < cols; ++c)
            {
                TileBase tile = allTiles[c + r * cols];
                if (tile != null)
                {
                    tmpList.Add(1);
                }
                else
                {
                    tmpList.Add(0);
                }
            }
            normalmap.Add(tmpList);
        }

        return normalmap;
    }

    private Tilemap NormalmapToTilemap(Normalmap normalmap, TileBase tile)
    {
        Tilemap tilemap = Instantiate(dummyTilemapPrefab, transform.position, Quaternion.identity);
        tilemap.ClearAllTiles();
        
        int rows = normalmap.Count;
        int cols = normalmap[0].Count;

        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < cols; ++c)
            {
                if (normalmap[r][c] == 1)
                {
                    tilemap.SetTile(new Vector3Int(c, r, 0), tile);
                }
            }
        }

        return tilemap;
    }

    public Vector3 GetCurrentOffset()
    {
        return defaultOffset;
    }
}
