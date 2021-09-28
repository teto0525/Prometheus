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

    //BGM(�����) ����
    public enum BGM_SOUND_TYPE
    {
        BGM_START,
        BGM_INGAME,
        BGM_RESULT
    }

    //EFT(ȿ����) ����
    public enum EFT_SOUND_TYPE
    {
        EFT_MONSTER,
        EFT_FOOTSTEP,
        EFT_BREATH,
        EFT_GUN,
        EFT_EXPLO,
        EFT_ROBOT_TRANSFORM
    }

    //EFT AudioSource ������Ʈ ���� ����
    public AudioSource audioS_EFT;
    //ȿ���� clip�� �������ִ� �迭
    public AudioClip[] eftAudio;

    //BGM AudioSource ������Ʈ ���� ����
    public AudioSource audioS_BGM;
    //����� clip�� �������ִ� �迭
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
        //type�� �ش�Ǵ� sound ���
        audioS_EFT.PlayOneShot(eftAudio[(int)type]);
        //PlayOneShot->�����Ŭ���� �߰��� ���� �ʰ� ������ �����
        
    }
}
