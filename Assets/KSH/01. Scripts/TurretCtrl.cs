using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    /* 기본 세팅 */
    [Header("General")]
    // 타겟위치
    private Transform target;
    // 범위
    public float range = 15f;
    // 타겟 발견시 시간, 타겟 상실
    float TimeStartedDetection;
    float TimeLostDetection;

    /* Public */
    [Header("Unity Setup Fields")]
    // 도는 구조
    public Transform partToRotate;
    // 도는 속도
    public float rotSpeed = 2;

    /* 레이저 */
    [Header("Use Laser")]
    public bool useLaser = false;
    public Transform Firepos;
    public GameObject prefabs;
    public float MaxLength;
    private Ray ray;
    private Vector3 direction;
    private Quaternion rotation;
    private EGA_Laser LaserScript;
    public LineRenderer lineRenderer;
    //public ParticleSystem[] lineVfx;
    //public ParticleSystem impatctVfx;

    /* 이펙트 */
    [Tooltip("Random Damage Effect")]
    // 피격시
    public ParticleSystem[] RamdomhitSparks;
    // 플레이어 발견시
    public ParticleSystem[] OnDectVfx;
    public AudioClip OnDectSfx;


    [Tooltip("Explosion")]
    public GameObject exploVFX;
    public float exploRadius = 1.0f;
    Rigidbody rb;


    [Tooltip("Damaged")]
    int hitCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player")?.GetComponent<Transform>();

        // 레이저 스크립트 활성화
        LaserScript = Instantiate(prefabs).GetComponent<EGA_Laser>();
        LaserScript.gameObject.SetActive(false);
    }




    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SoundManager.soundManager.PlaySFX(SoundManager.SFX.EnemyDetection);
        //}

        // 타겟의 거리
        float distance = Vector3.Distance(target.transform.position, transform.position);
        //만약 타겟이 범위 안으로 들어오면
        if(distance <= range)
        {
            OnDetectedTarget();
        }
        if (distance > range)
        {
            OnLostTarget();
        }
    }


    bool isFind;
    /* 타겟 발견 */
    void OnDetectedTarget()
    {
        
        // 타겟 방향으로 돈다
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotSpeed).eulerAngles;
        // y축만 회전
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        // 레이저를 사용하겠다
        useLaser = true;
        //만약 레이저를 사용하면
        if (useLaser)
        {
            //레이저
            Laser();
        }

        if (isFind == false)
        {
            // 플레이어 발견 효과
            OnDectVfx[0].Play();
            isFind = true;
            print("파티클 플레이!!");

            if (OnDectSfx)
            {
                SoundManager.soundManager.PlaySFX(SoundManager.SFX.EnemyDetection);

            }
        }

       

        TimeStartedDetection = Time.time;
    }

    

    /* 피격 효과 */
    private void OnTriggerEnter(Collider coll)
    {
        hitCount++;

        if (RamdomhitSparks.Length > 0)
        {
            int n = Random.Range(0, RamdomhitSparks.Length - 1);
            RamdomhitSparks[n].Play();
        }

        // 만약 10발을 맞으면 파괴된다
        if (hitCount == 10)
        {
            Instantiate(exploVFX, transform.position, Quaternion.identity);
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            LaserScript.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    /* 레이저 공격 */
    void Laser()
    {
        //if (!lineRenderer.enabled)
        //{
        //    lineRenderer.enabled = true;
        //    //lineVfx[0].Play();
        //}

        // 레이저 스크립트 활성화
        //LaserScript = Instantiate(prefabs, Firepos.transform.position, Firepos.transform.rotation).GetComponent<EGA_Laser>();
        LaserScript.gameObject.SetActive(true);
        LaserScript.transform.position = Firepos.transform.position;
        LaserScript.transform.rotation = Firepos.transform.rotation;
        //// 시작은 FirePos
        //lineRenderer.SetPosition(0, Firepos.position);
        ////끝은 타겟
        //lineRenderer.SetPosition(1, target.position);

        Vector3 dir = Firepos.position - target.position;
        //impatctVfx.transform.position = target.position + dir.normalized;
        //mpatctVfx.transform.rotation = Quaternion.LookRotation(dir);


    }

    /* 타겟 상실 */
    void OnLostTarget()
    {
        OnDectVfx[0].Stop();
        LaserScript.gameObject.SetActive(false);
        TimeLostDetection = Time.time;
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
