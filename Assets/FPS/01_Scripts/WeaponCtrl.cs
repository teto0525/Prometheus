using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCtrl : MonoBehaviour
{
    /* ���� ���� */
    // �����̸�
    public string weaponName;
    // �� źâ�� źȯ��
    public int bulletsPerMag;
    // �ܿ� źȯ ����
    public int bulletsTotal;
    // ���� ������ źȯ ����
    public int currentBullets;
    // ��Ÿ�
    public float range;
    // �߻� ����
    public float fireRate;

    // �߻� �ӵ�
    private float fireTimer;
    // ����
    private bool isReloading = false;

    /* ����ĳ��Ʈ �������� */
    public Transform shootPoint;

    /* ��ƼŬ �ý��� */
    public ParticleSystem muzzleflash;

    /* �ִϸ��̼� */
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
        // �� �Ѿ��� �� = źâ �Ѿ� ����
        currentBullets = bulletsPerMag;
        // �� źâ ��Ȳ UI�� ��Ÿ����
        bulletsText.text = currentBullets + " / " + bulletsTotal;

        // �ִϸ��̼�
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // �ִϸ����� �� ���̾� ���� 
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        isReloading = info.IsName("Reload");

        // �߻� , ����
        if (Input.GetButton ("Fire1"))
        {
            // ���� źâ�� �Ѿ��� �ִٸ�
            if(currentBullets > 0)
            {
                // �߻��϶�
                Fire();
            }
            // �׷��� �ʴٸ� �����϶�
            else
            {
                // ���� �ִϸ��̼�
                DoReload();
            }
        }
        
        // R��ư ������ �����϶�
        if (Input.GetKeyDown(KeyCode.R))
        {
            DoReload();
        }

        // ���� �߻�ӵ��� �߻簣�ݺ��� ���ٸ�
        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }

    }

    /* Fire */
    private void Fire()
    {
        // �߻� ���ݺ��� �߻� �ӵ����� ������ fire �Լ��� �����Ѵ�
        // ���� ���̸� �߻����� ���Ѵ�
        if (fireTimer < fireRate)
        {
            return;
        }

        Debug.Log("Shot Fired!");
        // ����ĳ��Ʈ (��������, ����, ������ ��ü�� hit�� ����, ����)
        RaycastHit hit;
        // ����ĳ��Ʈ�� �����Ǿ��� ���� ����ǰ� �ʹ�
        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            Debug.Log("Hit!");
        }
        // �ʱ�ȭ
        currentBullets--;
        fireTimer = 0.0f;

        // ����� ���
        audio.PlayOneShot(shootsound);
        anim.CrossFadeInFixedTime("Fire", 0.01f);
        muzzleflash.Play();

        // źâ UI �ݿ�
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

    // �ִϸ��̼� ������Ʈ���� ��ũ��Ʈ �ڵ� ȣ��
    public void Reload()
    {
        // ���� �ؾ��� �Ѿ� = źâ - �ܷ� �Ѿ�
        int bulletsToReload = bulletsPerMag - currentBullets;

        // ���� ���� �ؾ��� �Ѿ��� �� �Ѿ˺��� ���ٸ�
        //if (bulletsToReload > bulletsTotal)
        //{

        //    bulletsToReload = bulletsTotal;
        //}

        currentBullets += bulletsToReload;
        bulletsTotal -= bulletsToReload;
        bulletsText.text = currentBullets + " / " + bulletsTotal;

    }
    

}
