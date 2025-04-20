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
    public GameObject fishSign;
    public GameObject zzi;


    private void Start()
    {
        fishSign.SetActive(false);
    }

    private void Update()
    {
        Waiting();
        CatchFish();
    }

    private void CatchFish()
    {
        if (GameManager.instance.currentState == FishingState.CanCatch)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameManager.instance.ChangeFishingState();
                fishSign.SetActive(false);
            }
        }
        
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
                 GameManager.instance.ChangeCanCatchState();
                 VisibleSign();
                 StartCoroutine(CanCatchCoroutine());
             }
             waitTime = 0;
             fishTime = Random.Range(2f, 6f);
        }
    }

    private void VisibleSign()
    {
        SoundManager.instance.PlaySFX("SFX2");
        fishSign.SetActive(true);
        fishSign.transform.position = new Vector3(zzi.transform.position.x, zzi.transform.position.y + 3f, 0);
    }
    
    IEnumerator CanCatchCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        if (GameManager.instance.currentState == FishingState.CanCatch)
        {
            GameManager.instance.ChangeWaitState();
        }
        fishSign.SetActive(false);
    }
}
