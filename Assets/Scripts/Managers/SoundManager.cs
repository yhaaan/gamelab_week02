using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource bgmSource;
    private AudioSource sfxSource;

    [Header("BGM Settings")] 
    public AudioClip bgmClip;
    
    [Header("SFX Settings")]
    public List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> sfxDict;
    
    public Slider sfxSlider;
    public Slider bgmSlider;
    
    public float BGMVolume;
    public float SFXVolume;
    private void Awake()
    {
        instance = this;
        
        sfxDict = new Dictionary<string, AudioClip>();
        foreach (var clip in sfxClips)
        {
            sfxDict[clip.name] = clip;
        }
    }

    private void Start()
    {
       
        GameObject bgmobj = new GameObject("BGM AudioSource");
        bgmSource = bgmobj.AddComponent<AudioSource>();
        bgmobj.transform.parent = transform;
        
        GameObject sfxobj = new GameObject("SFX AudioSource");
        sfxSource = sfxobj.AddComponent<AudioSource>();
        sfxobj.transform.parent = transform;
        sfxSource.volume = 0.1f;
        SetBGMVolume();
        SetSFXVolume();
        PlayBGM();
    }

    private void PlayBGM()
    {
        if (bgmSource && bgmClip)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.volume = 0.15f;
            bgmSource.Play();
        }
    }
    
    // 🎧 효과음 재생
    public void PlaySFX(string soundName)
    {
        if (sfxDict.ContainsKey(soundName))
        {
            sfxSource.PlayOneShot(sfxDict[soundName]);
        }
        else
        {
            Debug.LogWarning($"[SoundManager] 효과음 '{soundName}'을 찾을 수 없습니다!");
        }
    }
    
    public void MuteBGM()
    {
        bgmSource.mute = !bgmSource.mute;
    }
    
    public void SetBGMVolume()
    {
        BGMVolume = bgmSlider.value;
        bgmSource.volume = BGMVolume;
    }
    
    public void SetSFXVolume()
    {
        SFXVolume = sfxSlider.value;
        sfxSource.volume = SFXVolume;
    }
}
