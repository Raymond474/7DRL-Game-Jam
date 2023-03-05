using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    public string itemName;
    public int value;
    public int minXValueSpawn;
    public float spawnRate;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.sprite;
        itemName = itemData.name;
        value = itemData.value;
        minXValueSpawn = itemData.minXValueSpawn;
    }
}
