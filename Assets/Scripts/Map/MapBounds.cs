using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBounds : MonoBehaviour
{
    public static MapBounds instance;

    public Tilemap tilemap;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    void Awake()
    {
        instance = this;

        Bounds bounds = tilemap.localBounds;
        minBounds = bounds.min;
        maxBounds = bounds.max;
    }
}
