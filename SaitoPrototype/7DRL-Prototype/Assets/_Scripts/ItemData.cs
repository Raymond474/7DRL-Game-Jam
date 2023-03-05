using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "New ItemData")]
public class ItemData : ScriptableObject
{
    public Sprite sprite;
    public string itemName;
    public int value;
    public int minXValueSpawn; //minimun x value (from beach) that item can spawn. Use to have items only being able to spawn out further in level

    public int maxXValueSpawn;

    [Range(0, 100)] public float spawnRate;
    [Range(0, 100)] public int minFromLastSpawn;
}
