using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaitFish : MonoBehaviour
{
    [SerializeField]
    private float waitTime = 0f;
    [SerializeField]
    private float fishTime = 0f;
    


    private void Update()
    {
        Waiting();
    }


    public void StartWait()
    {
        fishTime = Random.Range(2f, 6f);
        waitTime = 0;
    }


    private void Waiting()
    {
        if(GameManager.instance.currentState != FishingState.Wait) return; 
        waitTime += Time.deltaTime;
        if (waitTime >= fishTime)
        {
             int random = Random.Range(0, 2);
             if (random == 1)
             {
                 Debug.Log("Fish");
                 //GameManager.instance.ChangeFishingState();
                 //GameManager.instance.StartFishing(); <- GameManager에서 해줍시다 . .. .
             }
             else
             {
                 waitTime = 0;
             }
        }
    }
}
