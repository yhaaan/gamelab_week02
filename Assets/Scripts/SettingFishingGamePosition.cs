using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingFishingGamePosition : MonoBehaviour
{
    public GameObject virtualFishingGame;
    public GameObject virtualBar;


    private void Start()
    {
        virtualFishingGame.SetActive(false);
        
    }

    public void OnSet()
    {
        if(virtualFishingGame.activeSelf)
            virtualFishingGame.SetActive(false);
        else
            virtualFishingGame.SetActive(true);
    }



    public void PosSet(BaseEventData data)
    {
        Debug.Log("Pos set");
        PointerEventData pointerEventData = data as PointerEventData;
        
        Vector2 pointerPos = pointerEventData.position;
        Camera cam = Camera.main;
        
        float zCoord = cam.WorldToScreenPoint(virtualBar.transform.position).z;
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(pointerPos.x, pointerPos.y, zCoord));
        virtualBar.transform.position = worldPos;
    }
}
