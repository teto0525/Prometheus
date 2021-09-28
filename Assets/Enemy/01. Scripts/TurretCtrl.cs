using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    // 타겟위치
    private Transform target;

    [Header("General")]
    // 범위
    public float range = 15f;

    /* 총알 */
    [Header("Use Bullets (default)")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    /* 레이저 */
    [Header("Use Laser")]
    // 레이저 
    public bool useLaser = false;
    // 레이저 총구
    public Transform Firepos;
    public LineRenderer lineRenderer;
    public GameObject buildEffect;


    [Header("Unity Setup Fields")]
    // 도는 구조
    public Transform partToRotate;
    // 도는 속도
    public float rotSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        // 타겟 특정하기
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void Update()
    {

        // 타겟의 거리
        float distance = Vector3.Distance(target.transform.position, transform.position);
        //만약 타겟이 범위 안으로 들어오면
        if(distance <= range)
        {
            // 타겟 방향으로 돈다
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotSpeed).eulerAngles;
            // y축만 회전
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            // 레이저를 사용하겠다
            useLaser = true;

            // 만약 레이저를 사용하면
            if (useLaser)
            {
                // 레이저
                Laser();
            }
            
        }

    }

    /* 레이저 공격 */
    void Laser()
    {
        // 시작은 FirePos
        lineRenderer.SetPosition(0, Firepos.position);
        // 끝은 타겟
        lineRenderer.SetPosition(1, target.position);

        //// 레이저 발사 = 총구에서 레이저 발생
        //GameObject Laser = (GameObject)Instantiate(buildEffect, Firepos.transform.position, Quaternion.identity);
        //Firepos.transform.position = transform.position;
        //Destroy(Laser, 5f);
    }

    void Shoot()
    {
        Debug.Log("Shoot");

    }
    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
