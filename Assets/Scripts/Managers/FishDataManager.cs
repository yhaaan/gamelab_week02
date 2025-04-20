using UnityEngine;
using System;

public class FishDataManager : MonoBehaviour
{
    public static FishDataManager instance;
    public FishData[] fishData;
    public Sprite[] fishImages;
    public Action fishDataLoadAction;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void Start()
    {
        LoadFishData();
    }

    private void LoadFishData()
    {
        // Resources 폴더에서 JSON 파일 읽기
        TextAsset jsonFile = Resources.Load<TextAsset>("FishData");
        
        if (jsonFile != null)
        {
            FishDataList fishList = JsonUtility.FromJson<FishDataList>("{\"fishArray\":" + jsonFile.text + "}");
            fishData = fishList.fishArray;
            Debug.Log("총 " + fishData.Length + "개의 물고기 데이터를 불러왔습니다.");
        }
        else
        {
            Debug.LogError("FishData.json 파일을 찾을 수 없습니다.");
        }
        fishDataLoadAction?.Invoke();
    }
    
    
}


[Serializable]
public class FishDataList
{
    public FishData[] fishArray;
}

[Serializable]
public class FishData
{
    public string Name;      // 물고기 이름
    public string Desc;      // 설명
    public int Price;        // 가격
    public int Season;       // 계절 (봄=1, 여름=2, 가을=4, 겨울=8, 모든 계절=0)
    public int Weather;      // 날씨 (상관없음=0, 맑음=1, 비=2, 바람=4, 눈=8)
    public float Level;      // 난이도 (0.5 ~ 2.0)
    public int Exp;          // 경험치
    public int Time;         // 시간 (0=아무때나, 1=오전 6시~오후 12시, 2=오후 12시~오전 2시, 3=오후 10시~오전 2시)
    public int Img;          // 이미지 ID (Unity에서 Sprite 로드 시 활용 가능)
}
