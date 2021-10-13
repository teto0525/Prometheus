using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    private void Awake()
    {
        if(soundManager  == null)
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM ����
    public enum BGM
    {
        Scifi
    }

    // ȿ���� ����
    public enum SFX
    {
        EnemyDetection,
        TurretExplo,
        Electric

    }

    //BGM AudioSource
    public AudioSource audio_BGM;
    public AudioClip[] bgmGroups;

    //SFX AudioSource
    public AudioSource audio_SFX;
    public AudioClip[] sfxGroups;

    // ����� 
    public void PlayBGM(BGM type)
    {
        audio_BGM.clip = bgmGroups[(int)type];
        audio_BGM.Play();
    }

    // ����� ����
    public void StopBGM()
    {
        audio_BGM.Pause();
    }

    // ȿ����
    public void PlaySFX(SFX type)
    {
        audio_SFX.PlayOneShot(sfxGroups[(int)type]);
    }
}
