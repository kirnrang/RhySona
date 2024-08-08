using UnityEngine;
using UnityEngine.Tilemaps;

public class tile_cordination_print : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private void Start()
    {
        PrintTilePositions();
    }

    private void PrintTilePositions()
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                for (int z = bounds.zMin; z < bounds.zMax; z++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, z);
                    TileBase tile = tilemap.GetTile(tilePosition);
                    if (tile != null)
                    {
                        Debug.Log($"Tile at {tilePosition}: {tile.name}");
                    }
                }
            }
        }
    }
}
