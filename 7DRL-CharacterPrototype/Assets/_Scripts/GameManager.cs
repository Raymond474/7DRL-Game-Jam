using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public GameDataManager gameDataManager;
    public GameTimers gameTimers;

    public float levelLength = 5; //seconds
    
    //public bool tideIn = true; //true= tide is in, false= tide is out

    public enum GameState
    {
        GameStart,
        TideIn,
        TideOut,
        TideMovingIn
    }

    public GameState gameState = GameState.GameStart;    

    private void OnEnable()
    {        
        EventManager.StartTideIn += StartTideIn;
        //EventManager.StartTideOut += StartTideOut;
        EventManager.EndTideCycle += EndTideCycle;
    }

    private void OnDisable()
    {
        EventManager.StartTideIn -= StartTideIn;
        //EventManager.StartTideOut -= StartTideOut;
        EventManager.EndTideCycle -= EndTideCycle;
    }    
    
    private void Awake()
    {
        levelGenerator = GetComponent<LevelGenerator>();
        gameDataManager = GetComponent<GameDataManager>();
        gameTimers = GetComponent<GameTimers>();        
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        StartTideCycle();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Return))
        {
            //StartFirstTideCycle();
            //StartTideCycle();            

            StartTideCycle();
        }*/
    }
    void StartTideCycle()
    {
        if (gameState == GameState.TideIn || gameState == GameState.GameStart)
        {
            if (gameDataManager.tideCycleCounter == 0)
            {
                StartFirstTideCycle();
            }
            else
            {
                //StartTideOut();
            }

            gameState = GameState.TideOut;
            gameDataManager.tideCycleCounter++;
        }
    }

    public void StartFirstTideCycle()
    {
        EventManager.StartTideCycleTimerEvent(levelLength);
        EventManager.StartTideOutEvent();
        
    }
    /*void StartTideOut()
    {
        EventManager.StartTideOutEvent();
        EventManager.StartTideCycleTimerEvent(levelLength);
    }*/

    void StartTideIn()
    {
        EventManager.StartTideMovingInEvent();
        gameState = GameState.TideMovingIn;
    }

    void EndTideCycle()
    {
        gameState = GameState.TideIn;
    }



    

    
}
