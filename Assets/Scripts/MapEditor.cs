using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Normalmap = System.Collections.Generic.List<System.Collections.Generic.List<int>>;

public class MapModel
{
    public string name;
    public Tilemap map;
    public TileBase tile;
    public Vector3 offsetFromOrigin;
    public Vector3 offsetToNextMap;

    public MapModel(string name, Tilemap map, TileBase tile, Vector3 offsetFromOrigin, Vector3 offsetToNextMap)
    {
        this.name = name;
        this.map = map;
        this.tile = tile;
        this.offsetFromOrigin = offsetFromOrigin;
        this.offsetToNextMap = offsetToNextMap;
    }
}

public class MapModelSet
{
    public List<MapModel> mapModels;
    public Vector3 startPos;

    public MapModelSet(List<MapModel> mapModels, Vector3 startPos)
    {
        this.mapModels = mapModels;
        this.startPos = startPos;
    }
}

public class MapEditor : MonoBehaviour
{
    // Default offset between different map models
    public Vector3 defaultOffset;

    // Assets
    public List<Tilemap> baseTilemaps;
    public List<TileBase> baseTiles;

    // Map set
    private List<MapModelSet> mapModelSet;
    private List<Tilemap> tilemapSet;
    private int currentMapIndex = 0;
    private int currentSetIndex;

    // Flag
    public GameObject flagPrefab;
    private GameObject flag;
    private int flagMapIndex;

    private void Awake()
    {
        mapModelSet = new List<MapModelSet>();
        tilemapSet = new List<Tilemap>();
    }

    public void InitMapSets()
    {
        List<MapModel> set0 = new List<MapModel>();
        set0.Add(new MapModel("LightMap", baseTilemaps[0], baseTiles[0], Vector3.zero, defaultOffset));
        set0.Add(new MapModel("DarkMap", baseTilemaps[1], baseTiles[1], defaultOffset, -defaultOffset));
        mapModelSet.Add(new MapModelSet(set0, new Vector3((float)105.5, (float)1.4, 0)));
    }

    public Vector3 UseMapSet(int index)
    {
        // Clear current maps
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        tilemapSet.Clear();

        // Use appropriate map set
        currentSetIndex = index;
        foreach (MapModel model in mapModelSet[index].mapModels)
        {
            Tilemap tmp = MapModelToTilemap(model);
            tmp.transform.parent = transform;
            tmp.transform.position += model.offsetFromOrigin;
            tmp.name = model.name;
            tilemapSet.Add(tmp);
        }
        return mapModelSet[index].startPos;
    }
    
    public bool SwitchToNextMap(Vector3 curPos, out Vector3 offset)
    {
        MapModelSet currentSet = mapModelSet[currentSetIndex];
        MapModel currentMap = currentSet.mapModels[currentMapIndex];

        int nextMapIndex = (currentMapIndex + 1) % currentSet.mapModels.Count;
        Tilemap nextMap = tilemapSet[nextMapIndex];
        offset = currentMap.offsetToNextMap;

        if (nextMap.GetTile(nextMap.WorldToCell(curPos + offset)) == null)
        {
            currentMapIndex = nextMapIndex;
            return true;
        }

        return false;
    }

    public void SetFlag(Vector3 pos)
    {
        if (flag == null)
        {
            flag = Instantiate(flagPrefab, pos, Quaternion.identity);
        }
        else
        {
            flag.transform.position = pos;
        }

        flagMapIndex = currentMapIndex;
    }

    public Vector3 SwitchToFlag()
    {
        currentMapIndex = flagMapIndex;
        return flag.transform.position;
    }

    private Tilemap MapModelToTilemap(MapModel model)
    {
        Tilemap tilemap = Instantiate(model.map, transform.position, Quaternion.identity);
        tilemap.ClearAllTiles();

        Normalmap normalmap = TilemapToNormalmap(model.map);

        int rows = normalmap.Count;
        int cols = normalmap[0].Count;

        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < cols; ++c)
            {
                if (normalmap[r][c] == 1)
                {
                    tilemap.SetTile(new Vector3Int(c, r, 0), model.tile);
                }
            }
        }

        return tilemap;
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
}
