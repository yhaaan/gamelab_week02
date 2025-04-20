using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class DogamManager : MonoBehaviour
{
    public static DogamManager instance;


    public CanvasGroup dogamUI;
    [SerializeField] private DogamFish[] fish;
    private GameObject doGam;
    private GameObject infoUi;
    private Image icon;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI infoText;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        doGam = dogamUI.transform.GetChild(1).gameObject;
        infoUi = dogamUI.transform.GetChild(2).gameObject;
        icon = infoUi.transform.GetChild(2).GetComponent<Image>();
        nameText = infoUi.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        infoText = infoUi.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        infoUi.SetActive(false);
        FishDataManager.instance.fishDataLoadAction += InitDogam;
    }

    private void InitDogam()
    {
        List<DogamFish> fishList = new List<DogamFish>();
        foreach (var data in doGam.transform.GetComponentsInChildren<DogamFish>())
        {
            fishList.Add(data);
        }

        fish = fishList.ToArray();
        Debug.Log(fish.Length);
        UpdateDogam();
    }

    public void UpdateDogam()
    {
        for (int i = 0; i < fish.Length; i++)
        {
            fish[i].SetFish(FishDataManager.instance.fishData[i]);
        }
    }


public void InfoUISet(FishData fishData)
    {
        infoUi.SetActive(true);
        string season = GetSeasonString(fishData.Season);
        string weather = GetWeatherString(fishData.Weather);
        string time = "";
        if (fishData.Time == 0) time = "아무때나";
        if (fishData.Time == 1) time = "오전 6시 ~ 오후 12시";
        if (fishData.Time == 2) time = "오후 12시 ~ 오전 2시";
        if (fishData.Time == 3) time = "오후 10시 ~ 오전 2시";
        icon.sprite = FishDataManager.instance.fishImages[fishData.Img - 1];
        nameText.text = fishData.Name;
        if (SaveDataManager.instance.fishCount[fishData.Img - 1] == 0)
        {
            icon.color = new Color(0f, 0f, 0f, 1);
            infoText.text = "설명 : \n ?????" + "\n\n"
                                            + "계절 : " + season + "\n날씨 : " + weather + "\n시간 : \n" + time;
        }
        else {
            icon.color = new Color(1f, 1f, 1f, 1);
            infoText.text = "설명 : \n" +fishData.Desc + "\n\n"
                              +"계절 : " +season + "\n날씨 : " + weather + "\n시간 : \n" + time;}
    }
    
    public void InfoUIOff()
    {
        infoUi.SetActive(false);
    }

    private string GetSeasonString(int season)
    {
        string seasonString = "";
        if ((season & 1) != 0)
        {
            seasonString += "봄 ";
        }
        if ((season & 2) != 0)
        {
            seasonString += "여름 ";
        }
        if ((season & 4) != 0)
        {
            seasonString += "가을 ";
        }
        if ((season & 8) != 0)
        {
            seasonString += "겨울 ";
        }
        if (season == 0)
        {
            seasonString = "모든 계절";
        }
        return seasonString;
    }

    private string GetWeatherString(int weather)
    {
        string seasonString = "";
        if ((weather & 1) != 0)
        {
            seasonString += "맑음 ";
        }
        if ((weather & 2) != 0)
        {
            seasonString += "비 ";
        }
        if ((weather & 4) != 0)
        {
            seasonString += "바람 ";
        }
        if ((weather & 8) != 0)
        {
            seasonString += "눈 ";
        }
        if (weather == 0)
        {
            seasonString = "모든 날씨";
        }
        return seasonString;
        
    }
}
