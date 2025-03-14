using System;
using UnityEngine;
using UnityEngine.Serialization;

public class FishingSystem : MonoBehaviour
{
    
    [HideInInspector]
    public FishMovement target;
    [HideInInspector]
    public FishingBar fishingBar;
    [HideInInspector]
    public FishingGage fishingGage;
    public bool isFishing = false;
    public FishData currentFishData;

    private void Awake()
    {
        target = GetComponentInChildren<FishMovement>();
        fishingBar = GetComponentInChildren<FishingBar>();
        fishingGage = GetComponentInChildren<FishingGage>();
    }

    private void Start()
    {
        isFishing = false;
    }

    
    public void StartFishing(FishData fishData)
    {
        isFishing = true;
        currentFishData = fishData;
        TargetSet();
        target.transform.localPosition = new Vector3(-0.1f, -1.4f, 0);
        fishingBar.transform.localPosition = new Vector3(-0.1f, -1.4f, 0);
        fishingGage.GetComponent<FishingGage>().completeTime = 1.5f;
        
    }

    public void FailFishing()
    {
        Debug.Log("Fail Fishing");
        //실패 UI 띄우기
        EndFishing();
    }
    public void CompleteFishing()
    {
        Debug.Log("CompleteFishing!");
        Debug.Log(currentFishData.Name);
        //도감에 추가 및 도감 업데이트 , 성공 UI 띄우기
        EndFishing();
    }

    public void EndFishing()
    {
        fishingBar.InitBar();
        isFishing = false;
    }
    
    
    private void TargetSet()
    {
        
        target.difficultyFactor = currentFishData.Level;
        target.isLegendary = false;
        if (currentFishData.Level > 100f)
        {
            target.isLegendary = true;
            target.LegendaryJump();
        }
    }

    public void TestButtonSetting(string fname)
    {
        FishData[] fd = FishDataManager.instance.fishData;
        foreach (var fish in fd)
        {
            if (fish.Name == fname)
            {
                StartFishing(fish);
            }
        }
        
    }

    
}


