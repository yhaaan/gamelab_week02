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
    public FishingState currentState;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public void ChangeState(FishingState newState)
    {
        currentState = newState;
    }
}
