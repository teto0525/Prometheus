using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePos;
    bool isFire = false;

    private RaycastHit hit;

    void Start()
    {
        // bullet이 바라보는 방향을 고정시키고 싶다

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(firePos.position, firePos.forward * 10.0f, Color.green);

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        // 만약 WeaponCtrl의 reload 상황일때
        //if (WeaponCtrl.Reload())
        //{

        //}
    }

    void Fire()
    {
        // Bullet 동적으로 생성 : Instantiate(프리팹, 위치, 각도)
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}

/*
    Quaternion 쿼터니언 (사원수 x, y, z, w) : 복소수 4차원 벡터

    x->y->z 오일러 회전  => 짐벌락(Gimbal Lock)
*/

