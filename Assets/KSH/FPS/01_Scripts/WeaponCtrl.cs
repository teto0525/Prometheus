using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCtrl : MonoBehaviour
{
    /* 무기 스펙 */
    // 무기이름
    public string weaponName;
    // 한 탄창의 탄환수
    public int bulletsPerMag;
    // 잔여 탄환 개수
    public int bulletsTotal;
    // 현재 장전된 탄환 개수
    public int currentBullets;
    // 사거리
    public float range;
    // 발사 간격
    public float fireRate;

    // 발사 속도
    private float fireTimer;
    // 장전
    private bool isReloading = false;

    /* 레이캐스트 시작지점 */
    public Transform shootPoint;

    /* 파티클 시스템 */
    public ParticleSystem muzzleflash;

    /* 애니메이션 */
    private Animator anim;

    /* Sound */
    public AudioClip reloadSound;
    public AudioClip shootsound;
    public new AudioSource audio;


    /* UI */
    public Text bulletsText;
    public GameObject Hpbar;

    // Start is called before the first frame update
    private void Start()
    {
        // 현 총알의 수 = 탄창 총알 개수
        currentBullets = bulletsPerMag;
        // 현 탄창 상황 UI로 나타내자
        bulletsText.text = currentBullets + " / " + bulletsTotal;

        // 애니메이션
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // 애니메이터 현 레이어 실행 
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        isReloading = info.IsName("Reload");

        // 발사 , 장전
        if (Input.GetButton ("Fire1"))
        {
            // 만약 탄창에 총알이 있다면
            if(currentBullets > 0)
            {
                // 발사하라
                Fire();
            }
            // 그렇지 않다면 장전하라
            else
            {
                // 장전 애니메이션
                DoReload();
            }
        }
        
        // R버튼 누르면 장전하라
        if (Input.GetKeyDown(KeyCode.R))
        {
            DoReload();
        }

        // 만약 발사속도가 발사간격보다 적다면
        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }

    }

    /* Fire */
    private void Fire()
    {
        // 발사 간격보다 발사 속도보다 적으면 fire 함수를 종료한다
        // 장전 중이면 발사하지 못한다
        if (fireTimer < fireRate)
        {
            return;
        }

        Debug.Log("Shot Fired!");
        // 레이캐스트 (시작지범, 방향, 감지된 객체를 hit에 저장, 길이)
        RaycastHit hit;
        // 래이캐스트에 감지되었을 때만 실행되고 싶다
        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            Debug.Log("Hit!");
        }
        // 초기화
        currentBullets--;
        fireTimer = 0.0f;

        // 오디오 재생
        audio.PlayOneShot(shootsound);
        anim.CrossFadeInFixedTime("Fire", 0.01f);
        muzzleflash.Play();

        // 탄창 UI 반영
        bulletsText.text = currentBullets + " / " + bulletsTotal;

    }
    private void DoReload()
    {
        if(!isReloading && currentBullets < bulletsPerMag && bulletsTotal > 0)
        {
            anim.CrossFadeInFixedTime("Reload", 0.01f);
            audio.PlayOneShot(reloadSound);
        }
    }

    // 애니메이션 스테이트에서 스크립트 자동 호출
    public void Reload()
    {
        // 장전 해야할 총알 = 탄창 - 잔류 총알
        int bulletsToReload = bulletsPerMag - currentBullets;

        // 만약 장전 해야할 총알이 총 총알보다 많다면
        //if (bulletsToReload > bulletsTotal)
        //{

        //    bulletsToReload = bulletsTotal;
        //}

        currentBullets += bulletsToReload;
        bulletsTotal -= bulletsToReload;
        bulletsText.text = currentBullets + " / " + bulletsTotal;

    }
    

}
