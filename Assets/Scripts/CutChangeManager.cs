using System;
using UnityEngine;
using UnityEngine.Playables;

public class CutChangeManager : MonoBehaviour
{
    public static CutChangeManager instance;
    public PlayableDirector pd;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    public void ChangeCut()
    {
        pd.Play();
    }
}
