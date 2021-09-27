using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    // 타겟위치
    private Transform target;
    // 범위
    public float range = 15f;
    public string playerTag = "Player";
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
        }
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
