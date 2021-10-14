using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM 종류
    public enum BGM
    {
        Scifi
    }

    // 효과음 종류
    public enum SFX
    {
        EnemyDetection,
        TurretExplo,
        Electric,
        EnergyBar,
        
    }

    //BGM AudioSource
    public AudioSource audio_BGM;
    public AudioClip[] bgmGroups;

    //SFX AudioSource
    public AudioSource audio_SFX;
    public AudioClip[] sfxGroups;

    // 배경음 
    public void PlayBGM(BGM type)
    {
        audio_BGM.clip = bgmGroups[(int)type];
        audio_BGM.Play();
    }

    // 배경음 중지
    public void StopBGM()
    {
        audio_BGM.Pause();
    }

    // 효과음
    public void PlaySFX(SFX type)
    {
        audio_SFX.PlayOneShot(sfxGroups[(int)type]);
    }
}
