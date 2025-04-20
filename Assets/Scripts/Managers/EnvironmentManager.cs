using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager instance;
    public int day;
    public int hour;
    public int minute;
    public int season; // 0: spring, 1: summer, 2: fall, 3: winter
    public int weekday; // 1: monday, 2: tuesday, 3: wednesday, 4: thursday, 5: friday, 6: saturday, 0: sunday
    public int weather; // 1: sunny, 2: rainy, , 3 windy , 4: snowy
    [SerializeField]
    private float time;
    public float wholeTime;
    public Slider timeSpeedSlider;
    public TextMeshPro timeSpeedText;
    
    [Header("Texts")]
    public TextMeshProUGUI seasonText;
    public TextMeshProUGUI weekdayText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI weatherText;
    
    
    public float timeSpeed = 1f;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    private void Start()
    {
        weather = 1;
        minute = 0;
        hour = 6;
        timeSpeedSlider.value = 1f;
        timeSpeed = timeSpeedSlider.value;
        GameManager.instance.NextDayAction += WeekdaySet;
        GameManager.instance.NextDayAction += SeasonSet;
        GameManager.instance.NextDayAction += SeasonTextSet;
        GameManager.instance.NextDayAction += WeatherSet;
        GameManager.instance.NextDayAction += WeekdayTextSet;
        GameManager.instance.NextDayAction += () =>
        {
            wholeTime = 0;
        };

    }

    private void Update()
    {
        TimeFlow();
    }

    private void LateUpdate()
    {
        TimeTextSet();
    }

    private void TimeFlow()
    {
        time += Time.deltaTime * timeSpeed;
        wholeTime += Time.deltaTime * timeSpeed;

        // 7초마다 1분 증가
        if (time >= 7f)
        {
            minute+=10;
            time -= 7f;
        }

        // 6분(42초)마다 1시간 증가
        if (minute >= 60)
        {
            hour++;
            minute = 0;
        }

        // 26시간이 되면 다음 날로 넘어감
        if (hour >= 26)
        {
            
            GameManager.instance.NextDay();
            
        }
    }

    public void WeekdaySet()
    {
        hour = 6;
        minute = 0;
        day++;
        weekday = day % 7;
    } 
    
    public void SeasonSet()
    {
        if (day > 28)
        {
            day = 1;
            season += 1;
            season = season % 4;
        }
    }
    
    private void TimeTextSet()
    {
        timeText.color = Color.black;
        if (hour >= 22) timeText.color = Color.white;
        if (hour >= 24)
        {
            timeText.text = "오전 " + (hour-24).ToString("D2") + ":" + minute.ToString("D2");
            timeText.color = Color.red;
        }
        else if (hour >= 12)
        {
            if(hour == 12)
                timeText.text = "오후 " + hour + ":" + minute.ToString("D2");
            else
                timeText.text = "오후 " + (hour-12).ToString("D2") + ":" + minute.ToString("D2");
        }
        else 
            timeText.text = "오전 " + hour.ToString("D2") + ":" + minute.ToString("D2");
        
    }
    
    public void WeekdayTextSet()
    {
        if (weekday == 0) weekdayText.text = "일. " + day.ToString("D2");
        else if (weekday == 1) weekdayText.text = "월. " + day.ToString("D2");
        else if (weekday == 2) weekdayText.text = "화. " + day.ToString("D2");
        else if (weekday == 3) weekdayText.text = "수. " + day.ToString("D2");
        else if (weekday == 4) weekdayText.text = "목. " + day.ToString("D2");
        else if (weekday == 5) weekdayText.text = "금. " + day.ToString("D2");
        else if (weekday == 6) weekdayText.text = "토. " + day.ToString("D2");
    }
    public void SeasonTextSet()
    {
        if (season == 0) seasonText.text = "봄";
        else if (season == 1) seasonText.text = "여름";
        else if (season == 2) seasonText.text = "가을";
        else if (season == 3) seasonText.text = "겨울";
    }
    
    public void WeatherSet()
    {
        int random = Random.Range(0, 10);
        if (season == 0) //봄은 10%확률로 비가 옵니다.
        {
            if (random < 1) weather = 2; // rainy
            else weather = 1; // sunny
        }
        else if (season == 1) //여름은 20%확률로 비가 옵니다.
        {
            if (random < 4) weather = 2; // rainy
            else weather = 1; // sunny
        }
        else if (season == 2) //가을은 30%확률로 바람이 불고 10%확률로 비가 옵니다.
        {
            if (random < 3) weather = 3; // windy
            else if (random < 4) weather = 2; // rainy
            else weather = 1; // sunny
        }
        else if (season == 3) //겨울은 40%확률로 눈이 오고 30%확률로 바람이 붑니다
        {
            if (random < 4) weather = 4; // rainy
            else if (random < 7) weather = 3; // windy
            else weather = 1; // sunny
        }

        if (weather == 1)
        {
            weatherText.text = "맑음";
        }
        else if (weather == 2)
        {
            weatherText.text = "비";
        }
        else if (weather == 3)
        {
            weatherText.text = "바람";
        }
        else if (weather == 4)
        {
            weatherText.text = "눈";
        }
    }

    public void TimeSpeedSliderSet()
    {
        timeSpeed = timeSpeedSlider.value;
        timeSpeedText.text = "X" + timeSpeed;
    }
    
    
    
}
