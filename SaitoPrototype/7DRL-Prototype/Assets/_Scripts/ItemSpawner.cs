using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public List<ItemData> itemsToSpawn;
    public List<int> numItemsToSpawn; //# of items to spawn of matching index from itemsToSpawn
    public List<GameObject> dungeonsToSpawn;

    public GameObject itemPrefab;

    public GridManager gridManager;
    public int width;
    public int height;

    private void OnEnable()
    {
        EventManager.SpawnItems += SpawnItems;
    }

    private void OnDisable()
    {
        EventManager.SpawnItems -= SpawnItems;
    }

    private void Start()
    {       
        
    }

    void SpawnItems()
    {
        width = gridManager.levelGenerator.width;
        height = gridManager.levelGenerator.height;
        print(width + "  " + height);

        for (int i = 0; i < itemsToSpawn.Count; i++)
        {
            SpawnObjects(itemsToSpawn[i], numItemsToSpawn[i]);
        }        
    }

    void SpawnObjects(ItemData item, int ammount)
    {        
        
        int numToSpawn = ammount;
        print(numToSpawn);
        print(itemsToSpawn.Count);

        Vector3 lastSpawnedPos = new Vector3(0,0,0); 
        float lastSpawnDistance = 999;

        bool allSpawned = true;

        
            for (int x = item.minXValueSpawn; x < item.maxXValueSpawn; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (numToSpawn > 0 && gridManager.levelGrid[x, y] == 1)
                    {
                        float randomValue = Random.value;
                        //print(randomValue);
                        if ((item.spawnRate / 100) >= randomValue)
                        {
                            if (lastSpawnDistance > item.minFromLastSpawn)
                            {
                                print("Spawning");
                                Vector3 spawnPos = new Vector3(x, y, 0);
                                //lastSpawnDistance = Vector3.Distance(lastSpawnedPos, spawnPos);
                                print("SD" + lastSpawnDistance);
                                var spawnedItem = Instantiate(itemPrefab, spawnPos, Quaternion.identity, this.gameObject.transform);
                                spawnedItem.GetComponent<Item>().itemData = item;
                                lastSpawnedPos = spawnPos;
                                numToSpawn--;
                                gridManager.levelGrid[x, y] = 3;
                                if (numToSpawn <= 0)
                                    allSpawned = false;
                                break;
                            
                            }
                        }
                    }
                }
            } 
        
    }
}
