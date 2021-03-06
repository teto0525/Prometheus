using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    //회전 속도 변수 
    public float rotSpeed = 200f;

    //회전값 변수 
    float mx = 0;

    public bool useMouse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //게임 상태가 '게임중' 상태일 대만 조작할 수 잇게 한다. 
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        Vector2 stickpos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);


        //사용자의 마우스 입력을 받아 플레이어를 회전시키고 싶다. 
        //1. 마우스 좌우 입력값을 받는다. 
        float mouse_X;
        if(useMouse == true)
        {
            mouse_X = Input.GetAxis("Mouse X");
        }
        else
        {
            mouse_X = stickpos.x;
        }

        //1-1 회전 값 변수에 마우스 입력 값 만큼 미리 누적 시킨다. 
        mx += mouse_X * rotSpeed * Time.deltaTime;

        //2. 회전 방향으로 물체를 회전시킨다. 
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
