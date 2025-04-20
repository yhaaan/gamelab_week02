using UnityEngine;


public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;
    public int[] fishCount = new int[50];
    
    
    private void Awake()
    {
        instance = this;
        fishCount = new int[50];
        LoadData();
    }

    private void Start()
    {
        Screen.SetResolution(1920, 800, FullScreenMode.FullScreenWindow);
    }
    
    public void LoadData()
    {
        if (PlayerPrefs.HasKey("GameData"))
        {
            Debug.Log("데이터 불러오기 성공");
            for (int i = 0; i < 50; i++)
            {
                fishCount[i] = PlayerPrefs.GetInt("FishCount" + i);
            }
            EnvironmentManager.instance.day = PlayerPrefs.GetInt("Day");
            EnvironmentManager.instance.season = PlayerPrefs.GetInt("Season");
            EnvironmentManager.instance.SeasonTextSet();
            EnvironmentManager.instance.WeekdayTextSet();
            

        }
        else
        {
            PlayerPrefs.SetInt("GameData", 0);
            EnvironmentManager.instance.day = 1;
            EnvironmentManager.instance.season = 0;
            EnvironmentManager.instance.SeasonTextSet();
            EnvironmentManager.instance.WeekdayTextSet();
            DogamDataSave();
        }
    }

    public void DogamDataSave()
    {
        if (!PlayerPrefs.HasKey("GameData")) PlayerPrefs.SetInt("GameData", 0);
        for (int i = 0; i < 50; i++)
        {
            PlayerPrefs.SetInt("FishCount" + i, fishCount[i]);
        }
        PlayerPrefs.SetInt("Day", EnvironmentManager.instance.day);
        PlayerPrefs.SetInt("Season", EnvironmentManager.instance.season);
        PlayerPrefs.Save();
        
    }

    public void FishCountUp(int fishNum)
    {
        fishCount[fishNum-1]++;
        DogamDataSave();
    }
    
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        fishCount = new int[50];
        DogamManager.instance.UpdateDogam();
        DogamDataSave();
        PlayerPrefs.Save();
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        DogamDataSave();
    }
}
