using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //BGM(배경음) 종류
    public enum BGM_SOUND_TYPE
    {
        BGM_START,
        BGM_INGAME,
        BGM_RESULT
    }

    //EFT(효과음) 종류
    public enum EFT_SOUND_TYPE
    {
        EFT_MONSTER,
        EFT_FOOTSTEP,
        EFT_BREATH,
        EFT_GUN,
        EFT_EXPLO,
        EFT_ROBOT_TRANSFORM
    }

    //EFT AudioSource 컴포넌트 담을 변수
    public AudioSource audioS_EFT;
    //효과음 clip을 가지고있는 배열
    public AudioClip[] eftAudio;

    //BGM AudioSource 컴포넌트 담을 변수
    public AudioSource audioS_BGM;
    //배경음 clip을 가지고있는 배열
    public AudioClip[] bgmAudio;

    public void PlayBGM(BGM_SOUND_TYPE type)
    {
        audioS_BGM.clip = bgmAudio[(int)type];
        audioS_BGM.Play();

    }
    public void StopBGM()
    {
        audioS_BGM.Pause();
    }

    public void PlayEFT(EFT_SOUND_TYPE type)
    {
        //type에 해당되는 sound 출력
        audioS_EFT.PlayOneShot(eftAudio[(int)type]);
        //PlayOneShot->오디오클립을 중간에 끊지 않고 끝까지 출력함
        
    }
}
