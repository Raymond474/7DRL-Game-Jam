using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridManager : MonoBehaviour
{
    public LevelGenerator levelGenerator;

    public int[,] levelGrid; //1=ground, 2=obstacle, 3=item

    private void OnEnable()
    {
        EventManager.LevelGenerated += StartLevelGenerated;
    }

    private void OnDisable()
    {
        EventManager.LevelGenerated -= StartLevelGenerated;
    }

    private void Awake()
    {
        levelGenerator= GetComponent<LevelGenerator>();
        levelGrid = new int[levelGenerator.width, levelGenerator.height];
    }

    void StartLevelGenerated(int[,] map)
    {
        StartCoroutine(LevelGenerated(map));
    }
    

    IEnumerator LevelGenerated(int[,] map)
    {
        levelGrid = map;
        yield return new WaitForSeconds(0.25f);
        EventManager.SpawnItemsEvent();
    }
}
