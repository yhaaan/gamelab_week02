using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DogamFish : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    
    private Image icon;
    private TextMeshProUGUI cntText;
    public FishData fishData;
    
    private void Awake()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
        cntText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        
    }

    public void SetFish(FishData data)
    {
        fishData = data;
        icon.sprite = FishDataManager.instance.fishImages[data.Img-1];
        cntText.text = SaveDataManager.instance.fishCount[data.Img-1].ToString();
        if (SaveDataManager.instance.fishCount[data.Img - 1] == 0)
        {
            icon.color = new Color(0f, 0f, 0f, 1);
        }
        else icon.color = new Color(1f, 1f, 1f, 1);
    }


    public  void OnPointerEnter(PointerEventData eventData)
    {
        DogamManager.instance.InfoUISet(fishData);
    }
    
    public  void OnPointerExit(PointerEventData eventData)
    {
        DogamManager.instance.InfoUIOff();
    }
}
