using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    [Header("Terrain Gen")]
    public int width;
    public int height;
    //public float smoothness;
    public float flowRate;
    public float seed;
    int[] perlinHeightList;

    [Header("Cave Gen")]
    //[Range(0, 1)] [SerializeField] float modifier;
    [Range(0, 100)] public int randomFillPercent;
    public int smoothAmount;

    [Header("Tile")]
    [SerializeField] TileBase groundTile;
    [SerializeField] TileBase obstacleTile;
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap obstacleTilemap;
    [SerializeField] Tilemap objectTilemap;

    [Header("Objects")]
    [SerializeField] List<TileBase> plants;

    public int[,] map; //1=ground, 2=obstacle

    public GameObject boundarySquare;


    private void Start()
    {
        perlinHeightList = new int[width];
        Generation();
        boundarySquare.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Generation();
            //StartCoroutine(Flow());
        }
    }

    public void Generation()
    {
        seed = Time.time;
        if (seed == 0)
            seed = Random.Range(0, 999);
        ClearMap();
        map = GenerateArray(width, height, true);
        map = TerrainGeneration(map);
        SmoothMap(smoothAmount);
        Rendermap(map, groundTilemap, obstacleTilemap, groundTile, obstacleTile);
        SpawnDecorations();

        EventManager.LevelGeneratedEvent(map);

    }

    public int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = (empty) ? 0 : 1;
            }
        }

        return map;
    }

    public int[,] TerrainGeneration(int[,] map)
    {
        System.Random pseudoRandom = new System.Random(seed.GetHashCode());
        //int perlinHeight;
        for (int x = 0; x < width; x++)
        {
            //perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x / smoothness, seed) * height / 2);
            //perlinHeight += height / 2;
            //perlinHeightList[x] = perlinHeight;
            for (int y = 0; y < height; y++)
            {
                //map[x, y] = 1;
                //int caveValue = Mathf.RoundToInt(Mathf.PerlinNoise((x * modifier) + seed, (y * modifier) + seed));
                //map[x, y] = caveValue;  flips the tiles to empty, above does opposite.
                //map[x, y] = (caveValue == 1) ? 2 : 1;  

                map[x, y] = (pseudoRandom.Next(1, 100) < randomFillPercent) ? 1 : 2;
            }
        }
        return map;
    }

    void SmoothMap(int smoothAmount)
    {
        for (int i = 0; i < smoothAmount; i++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        map[x, y] = 1;
                    }
                    else
                    {
                        int surroundingGroundCount = GetSurroundingGroundCount(x, y);

                        if (surroundingGroundCount > 4)
                        {
                            map[x, y] = 1;
                        }
                        else if (surroundingGroundCount < 4)
                        {
                            map[x, y] = 2;
                        }
                    }
                }
            }
        }
    }

    int GetSurroundingGroundCount(int gridX, int gridY)
    {
        int groundCount = 0;

        for (int nebX = gridX - 1; nebX <= gridX + 1; nebX++)
        {
            for (int nebY = gridY - 1; nebY <= gridY + 1; nebY++)
            {
                if (nebX >= 0 && nebX < width && nebY >= 0 && nebY < height) //if inside map
                {
                    if (nebX != gridX || nebY != gridY) //excluding middle tile coordinate
                    {
                        if (map[nebX, nebY] == 1)
                        {
                            groundCount++;
                        }
                    }
                }
            }
        }

        return groundCount;
    }

    public void Rendermap(int[,] map, Tilemap groundTilemap, Tilemap obstacleTilemap, TileBase groundTile, TileBase obstacleTile)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                
                groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTile);
                
                if (map[x, y] == 2)
                {
                    obstacleTilemap.SetTile(new Vector3Int(x, y, 0), obstacleTile);
                }
            }
        }
    }

    void SpawnDecorations()
    {        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 1)
                {
                    int spawnRate = Random.Range(1, 101);
                    if(spawnRate == 1)
                    {
                        int randomListIndex = Random.Range(0, plants.Count);                        
                        
                        if(plants.Count > 0)
                            objectTilemap.SetTile(new Vector3Int(x, y, 0), plants[randomListIndex]);
                    }                    
                }
            }
        }
    }

    IEnumerator Flow()
    {
        if (seed == 10)
            seed = 1;
        Generation();
        yield return new WaitForSeconds(flowRate);
        seed++;
        StartCoroutine(Flow());
    }

    void ClearMap()
    {
        groundTilemap.ClearAllTiles();
        obstacleTilemap.ClearAllTiles();
        objectTilemap.ClearAllTiles();
    }

    /*public void AdjustSmoothness (float newValue)
    {
        smoothness = newValue * 1000;
    }

    public void AdjustWidth(string newValue)
    {
        width = int.Parse(newValue);
    }
    public void AdjustHeight(string newValue)
    {
        height = int.Parse(newValue);
    }*/
}
