using System;
using UnityEngine;



public enum FishingState
{
    Idle,
    CastLine,
    Wait,
    Fishing
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private WaitFish waitFish;
    public FishingState currentState;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        waitFish = FindFirstObjectByType<WaitFish>();
    }


    public void ChangeIdleState()
    {
        currentState = FishingState.Idle;
    }
    
    public void ChangeCastLineState()
    {
        currentState = FishingState.CastLine;
    }
    
    public void ChangeWaitState()
    {
        waitFish.StartWait();
        currentState = FishingState.Wait;
    }
    
    public void ChangeFishingState()
    {
        currentState = FishingState.Fishing;
    }
    
    
}
