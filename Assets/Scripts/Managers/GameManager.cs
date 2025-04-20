using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;



public enum FishingState
{
    Idle,
    CastLine,
    Wait,
    CanCatch,
    Fishing,
    GetFish
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private WaitFish waitFish;
    [SerializeField]
    private CastLine castLine;
    private FishingSystem fishingSystem;
    public FishingState currentState;
    public FishData[] currentFishPool;
    public CanvasGroup dogamCanvas;
    public Action NextDayAction;
    public CanvasGroup UiButtons;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Screen.SetResolution(1920, 800, FullScreenMode.FullScreenWindow);
        waitFish = FindFirstObjectByType<WaitFish>();
        castLine = FindFirstObjectByType<CastLine>();
        fishingSystem = FindFirstObjectByType<FishingSystem>();
        DogamOff();
        
        currentState = FishingState.Idle;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (dogamCanvas.alpha == 1)
            {
                DogamOff();
            }
            else
            {
                DogamOn();
            }
        }
    }


    public void DogamOn()
    {
        dogamCanvas.alpha = 1;
        dogamCanvas.blocksRaycasts = true;
        dogamCanvas.interactable = true;
    }
    
    public void DogamOff()
    {
        dogamCanvas.alpha = 0;
        dogamCanvas.blocksRaycasts = false;
        dogamCanvas.interactable = false;
    }
    
    
    public void ChangeIdleState()
    {
        currentState = FishingState.Idle;
        castLine.InitZzi();
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
    
    public void ChangeCanCatchState()
    {
        currentState = FishingState.CanCatch;
    }
    public void ChangeFishingState()
    {
        currentState = FishingState.Fishing;
        FishPoolSet();
        
        FishData randomFish = GetRandomFish();
        fishingSystem.StartFishing(randomFish);
    }

    public void ChangeGetFishState(bool isSucceed)
    {
        if (isSucceed)
            StartCoroutine(ChangeGetFishStateCoroutine());
        else
            StartCoroutine(ChangeFailFishStateCoroutine());
    }
    private FishData GetRandomFish()
    {
        int randomIndex = UnityEngine.Random.Range(0, currentFishPool.Length);
        return currentFishPool[randomIndex];
    }
    public void FishPoolSet()
    {
        List<FishData> fishPool = new List<FishData>();
        int currentSeason = EnvironmentManager.instance.season;
        int currentHour = EnvironmentManager.instance.hour;
        int currentWeather = EnvironmentManager.instance.weather;
        Dictionary<int, HashSet<int>> seasonFilters = new Dictionary<int, HashSet<int>>()
        {
            { 0, new HashSet<int> { 0, 1, 3, 5, 7, 9, 11 } }, // 봄
            { 1, new HashSet<int> { 0, 2, 3, 6, 7, 10, 14 } },       // 여름
            { 2, new HashSet<int> { 0, 4, 5, 6, 7, 12, 13, 14 } },    // 가을
            { 3, new HashSet<int> { 0, 8, 9, 10, 11, 12, 14 } }     // 겨울
        };
        
        // 1️⃣ 계절에 맞는 물고기 리스트 추가
        foreach (FishData data in FishDataManager.instance.fishData)
        {
            if (seasonFilters.ContainsKey(currentSeason) && seasonFilters[currentSeason].Contains(data.Season))
            {
                fishPool.Add(data);
            }
        }
        // 2️⃣ 현재 시간 조건에 맞지 않는 물고기 제거
        fishPool.RemoveAll(fish => !IsFishAvailableAtCurrentTime(fish.Time, currentHour));
        
        // 3️⃣ 현재 날씨 조건에 맞지 않는 물고기 제거
        fishPool.RemoveAll(fish => !IsFishAvailableAtCurrentWeather(fish.Weather, currentWeather));
        
        // 필요하면 배열로 변환
        currentFishPool = fishPool.ToArray();
    }
    
    // 특정 물고기가 현재 시간대에 잡힐 수 있는지 확인하는 함수
    private bool IsFishAvailableAtCurrentTime(int fishTime, int currentHour)
    {
        if (fishTime == 0) return true; // 0이면 언제든지 잡을 수 있음
        if (fishTime == 1 && 6 <= currentHour && currentHour < 18) return true;  // 오전 6시 ~ 오후 12시
        if (fishTime == 2 && 18 <= currentHour && currentHour <= 26) return true; // 오후 12시 ~ 오전 2시
        if (fishTime == 3 && 22 <= currentHour && currentHour <= 26) return true; // 오후 10시 ~ 오전 2시
        return false;
    }
    
    // 날씨 조건 체크 함수
    private bool IsFishAvailableAtCurrentWeather(int fishWeatherCondition, int currentWeather)
    {
        // 0이면 모든 날씨 가능
        if (fishWeatherCondition == 0)
            return true;

        // fishWeatherCondition이 현재 날씨를 포함하고 있는지 확인 (비트 연산)
        int weatherBit = 1 << (currentWeather - 1); // 현재 날씨 값을 비트로 변환
        return (fishWeatherCondition & weatherBit) != 0;
    }

    public void NextDay()
    {
        CutChangeManager.instance.ChangeCut();
    }
    
    
    IEnumerator ChangeGetFishStateCoroutine()
    {
        fishingSystem.OnGetFishUI();
        yield return new WaitForSeconds(1.0f);
        currentState = FishingState.GetFish;
    }
    
    IEnumerator ChangeFailFishStateCoroutine()
    {
        fishingSystem.OnFailFishUI();
        yield return new WaitForSeconds(1.0f);
        currentState = FishingState.GetFish;
    }

    public void NextDayActionStart()
    {
        NextDayAction?.Invoke();
        
    }

    public void QuitGame()
    {
        SaveDataManager.instance.DogamDataSave();
        Application.Quit();
    }

    public void WindowScreenMode()
    {
        if (Screen.fullScreen)
        {
            Screen.fullScreen = false;
            Screen.SetResolution(1920, 800, FullScreenMode.Windowed);

        }
        else
        {
            Screen.fullScreen = true;
            Screen.SetResolution(1920, 800, FullScreenMode.FullScreenWindow);
        }
    }

    public void UiButtonsOn()
    {
        if(UiButtons.alpha == 1)
        {
            UiButtons.alpha = 0;
            UiButtons.blocksRaycasts = false;
            UiButtons.interactable = false;
        }
        else
        {
            UiButtons.alpha = 1;
            UiButtons.blocksRaycasts = true;
            UiButtons.interactable = true;
        }
    }
    
}
