using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static event Action<int[,]> LevelGenerated;
    public static void LevelGeneratedEvent(int[,] map) { LevelGenerated?.Invoke(map); }

    public static event Action SpawnItems;
    public static void SpawnItemsEvent() { SpawnItems?.Invoke(); }

    public static event Action<Vector2Int, int> AddToGrid;
    public static void AddToGridEvent(Vector2Int gridPos, int itemInt) { AddToGrid?.Invoke(gridPos, itemInt); }

    public static event Action StartTideIn;
    public static void StartTideInEvent() { StartTideIn?.Invoke(); }

    public static event Action StartTideMovingIn;

    public static void StartTideMovingInEvent() { StartTideMovingIn?.Invoke(); }

    public static event Action StartTideOut;
    public static void StartTideOutEvent() { StartTideOut?.Invoke(); }

    public static event Action<float> StartTideCycleTimer;
    public static void StartTideCycleTimerEvent(float seconds) { StartTideCycleTimer?.Invoke(seconds); }

    public static event Action EndTideCycle;
    public static void EndTideCycleEvent() { EndTideCycle?.Invoke(); }


}
