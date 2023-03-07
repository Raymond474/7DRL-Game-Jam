using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimers : MonoBehaviour
{
    public float tideTimerCountdown;
    public UIManager uiManager;

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
        uiManager = GetComponent<UIManager>();
    }

    void TideTimerStart(float seconds)
    {
        StartCoroutine(TideTimerCountdown(seconds));
    }


    IEnumerator TideTimerCountdown(float seconds)
    {
        tideTimerCountdown = seconds;
        

        while (tideTimerCountdown >= 0)
        {
            tideTimerCountdown -= Time.deltaTime;
            uiManager.tideTimerSlider.value = tideTimerCountdown;

            if (tideTimerCountdown <= 0)
            {
                EventManager.StartTideInEvent();
            }

            yield return null;
        }
    }
}
