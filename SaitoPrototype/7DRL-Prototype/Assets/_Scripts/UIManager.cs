using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider tideTimerSlider;

    public GameTimers gameTimers;

    private bool stopTimer;
    public float tideTimer;

    private void OnEnable()
    {
        EventManager.StartTideCycleTimer += TideTimerStart;
    }

    private void OnDisable()
    {
        EventManager.StartTideCycleTimer -= TideTimerStart;
    }

    private void Awake()
    {
        gameTimers = GetComponent<GameTimers>();
    }

    private void Start()
    {
        stopTimer= false;
        
    }

    void TideTimerStart(float seconds)
    {        
        SetTideTimer(seconds);
    }

    void SetTideTimer(float seconds)
    {
        tideTimerSlider.maxValue = seconds;
        tideTimerSlider.value = seconds;
    }

    
}
