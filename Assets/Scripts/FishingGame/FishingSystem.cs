using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
    public CanvasGroup getFishUI;
    public Image fishImage;
    public TextMeshProUGUI fishName;
    public TextMeshProUGUI titleText;
    public GameObject newText;
    public GameObject virtualFishGamePos;

    private void Awake()
    {
        target = GetComponentInChildren<FishMovement>();
        fishingBar = GetComponentInChildren<FishingBar>();
        fishingGage = GetComponentInChildren<FishingGage>();
    }

    private void Start()
    {
        isFishing = false;
        OffGame();
        OffGetFishUI();
    }
    
    public void Update()
    {
        if (GameManager.instance.currentState == FishingState.GetFish)
        {
            if (Input.GetMouseButtonUp(0))
            {
                OffGetFishUI();
                EndFishing();
            }
        }
    }

    
    public void StartFishing(FishData fishData)
    {
        isFishing = true;
        currentFishData = fishData;
        TargetSet();
        target.transform.localPosition = new Vector3(-0.1f, -1.4f, 0);
        fishingBar.transform.localPosition = new Vector3(-0.1f, -1.4f, 0);
        fishingGage.GetComponent<FishingGage>().completeTime = 1.5f;
        OnGame();
        
    }

    
    public void FailFishing()
    {
        SoundManager.instance.PlaySFX("SFX4");
        isFishing = false;
        fishingBar.InitBar();
        
        GameManager.instance.ChangeGetFishState(false);
        
        
        EndFishing();
    }
    public void CompleteFishing()
    {
        SoundManager.instance.PlaySFX("SFX3");
        isFishing = false;
        fishingBar.InitBar();
        SaveDataManager.instance.FishCountUp(currentFishData.Img);
        DogamManager.instance.UpdateDogam();
        GameManager.instance.ChangeGetFishState(true);
    }

    public void EndFishing()
    {
        fishingBar.InitBar();
        GameManager.instance.ChangeIdleState();
        isFishing = false;
        OffGame();
        
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

    public void OffGame()
    {
        transform.position = new Vector3(10.19f, 50, 0);
    }

    public void OnGame()
    {
        SoundManager.instance.PlaySFX("SFX6");
        transform.position = virtualFishGamePos.transform.position;
        
    }


    public void OnGetFishUI()
    {
        
        getFishUI.alpha = 1;
        getFishUI.blocksRaycasts = true;
        getFishUI.interactable = true;
        titleText.text = "성공!";
        fishImage.sprite = FishDataManager.instance.fishImages[currentFishData.Img - 1];
        fishName.text = currentFishData.Name;
        if (SaveDataManager.instance.fishCount[currentFishData.Img - 1] == 1)
        {
            SoundManager.instance.PlaySFX("SFX1");
            newText.SetActive(true);
        }
        else newText.SetActive(false);
    }
    
    public void OffGetFishUI()
    {
        getFishUI.alpha = 0;
        getFishUI.blocksRaycasts = false;
        getFishUI.interactable = false;
        newText.SetActive(false);
    }

    public void OnFailFishUI()
    {
        getFishUI.alpha = 1;
        getFishUI.blocksRaycasts = true;
        getFishUI.interactable = true;
        fishImage.sprite = null;
        titleText.text = "실패...";
        fishName.text = "다음에 더 잘해보세요!";
        newText.SetActive(false);
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


