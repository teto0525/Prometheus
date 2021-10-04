using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //총알공장
    public GameObject bulletFactory;
    //총구
    public Transform trRFirePos;

    public GameObject fragmentFactory;

    //왼손 Transform.
    public Transform trLeft;

    //오른손 Transform.
    public Transform trRight;

    // Start is called before the first frame update
    void Start()
    {
        //trFirePos = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //게임 상태가 '게임중' 상태일 대만 조작할 수 잇게 한다. 
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        Vector2 stickpos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        print(stickpos.x + " , " + stickpos.y);

        #region Bullet관련

        //마우스 왼쪽 버튼을 누르면 총알을 발사하고 싶다. 
        //1.만약에 마우스 오른쪽 버튼을 누르면
        //if (Input.GetButtonDown("Fire1")) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))

        //{
           
        //}

        //마우스 오른쪽 버튼을 누르면 Ray를 활용해서
        //맞은편 파편 효과를 보여준다. .
        //1. 만약에 마우스 오른쪽 버튼을 누르면


        //마우스 오른쪽 버튼을 누르면 시선이 바라보는 방향으로 총을 발사하고 싶다. 
        // 마우스 오른쪽 버튼을 입력받는다. 

        #endregion

        if (Input.GetButtonDown("Fire1")  || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            FireRay(); 
            FireBullet();
        }
    }

    void FireRay()
    {
        //2. 카메라 위치, 카메라 앞방향으로 나가는 Ray를 만든다.
        Ray ray = new Ray(trRFirePos.position, trRFirePos.forward );
        
        //3. 만약에 Ray를 발사해서 어딘가에 부딪혔다면
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //4. 파편 효과공장에서 파편을 만든다. 
            GameObject fragment = Instantiate(fragmentFactory);
            //5. 만든 효과를 부딪힌 위치에 놓는다. 
            fragment.transform.position = hit.point;
            //6. 만든효과의 앞 방향을 부딪힌 위치에 normal 벡터로 셋팅
            fragment.transform.forward = hit.normal;
            //7.2초뒤에 파괴하자. 
            Destroy(fragment, 2);

            #region enemy관련

            ////8.만약에 맞은 놈이 Drone 이라면
            //if (hit.transform.name.Contains("Enemy"))
            //{
            //    //9. 파괴해라.
            //    Destroy(hit.transform.gameObject);
            //}


            #endregion
        }

    }

    #region fireBullet

    void FireBullet()
    {
        //2. 총알 공장에서 총알을 만든다.
        GameObject bullet = Instantiate(bulletFactory);
        //3. 만들어진 총알을 총구에 놓는다. 
        bullet.transform.position = trRight.position;
        //4. 만들어진 총알의 앞방향을 총구의 앞방향으로 한다. 
        bullet.transform.forward = trRight.forward;
    }
    #endregion
}
