using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Tide : MonoBehaviour
{
    public Vector3 startPosTideIn;
    public Vector3 endPosTideIn;
    public Vector3 startPosTideOut;
    public Vector3 endPosTideOut;
    public float movementDuration = 1;
    //public float elapsedTime;
    //public bool tideIn = false;
    //public bool tideOut = false;

    private void OnEnable()
    {
        //EventManager.StartTideIn += StartTideIn;
        EventManager.StartTideOut += StartTideOut;
        EventManager.StartTideMovingIn += StartTideMovingIn;
    }

    private void OnDisable()
    {
        //EventManager.StartTideIn -= StartTideIn;
        EventManager.StartTideOut -= StartTideOut;
        EventManager.StartTideMovingIn -= StartTideMovingIn;
    }

    private void Start()
    {
        //startPos= transform.position;
    }

    private void Update()
    {
        /*if (tideIn)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / movementDuration;

            transform.position = Vector3.Lerp(startPosTideIn, endPosTideIn, percentageComplete);
            print(percentageComplete);
            if(percentageComplete >= 1)
            {
                tideIn = false;
                elapsedTime = 0;
            }
        }

        if (tideOut)
        {
            
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / movementDuration;

            transform.position = Vector3.Lerp(startPosTideOut, endPosTideOut, percentageComplete);
            print(percentageComplete);
            if (percentageComplete >= 1)
            {
                tideOut = false;
                elapsedTime = 0;
                
            }
        }*/        
    }

    void StartTideMovingIn()
    {
        StartCoroutine(MoveTideIn());
    }


    void StartTideOut()
    {
        StartCoroutine(MoveTideOut());
    }

    /*IEnumerator MoveTideOut()
    {
        float startTime = Time.time;

        while(Time.time < startTime + movementDuration)
        {
            transform.position = Vector3.Lerp(startPosTideOut, endPosTideOut, (Time.time - startTime) / movementDuration);
            yield return null;
        }

        float time = 0;
        while (time < movementDuration)
        {
            transform.position = Vector3.Lerp(startPosTideOut, endPosTideOut, time/movementDuration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = endPosTideOut;
        
    }*/

    IEnumerator MoveTideOut()
    {
        float startTime = Time.time;

        while (Time.time < startTime + movementDuration)
        {
            transform.position = Vector3.Lerp(startPosTideOut, endPosTideOut, (Time.time - startTime) / movementDuration);
            yield return null;
        }    
        
        yield return null;        
    }

    IEnumerator MoveTideIn()
    {
        float startTime = Time.time;

        while (Time.time < startTime + movementDuration)
        {
            transform.position = Vector3.Lerp(startPosTideIn, endPosTideIn, (Time.time - startTime) / movementDuration);
            yield return null;
        }
        yield return null;

        EventManager.EndTideCycleEvent();
    }

}
