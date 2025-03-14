using System;
using UnityEngine;

public class FishingGage : MonoBehaviour
{
    public float maxCompleteTime = 5f;
    public float completeTime = 0f;
    public bool TEST_MODE = false;
    private SpriteRenderer sr;
    private FishingSystem fishingSystem;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        fishingSystem = transform.parent.GetComponent<FishingSystem>();
        completeTime = 0;
        GageSetting();
    }
    
    private void GageSetting()
    {
        sr.size = new Vector2(1, completeTime/maxCompleteTime);
    }
    
    public void GageDecrease()
    {
        if(TEST_MODE) return;
        completeTime -= Time.deltaTime;
        if (completeTime <= 0)
        {
            completeTime = 0;
            fishingSystem.FailFishing();
        }
        GageSetting();
    }
    
    public void GageIncrease()
    {
        if(TEST_MODE) return;
        completeTime += Time.deltaTime;
        if (completeTime >= maxCompleteTime)
        {
            completeTime = maxCompleteTime;
            fishingSystem.CompleteFishing();
        }
        GageSetting();
    }
}
