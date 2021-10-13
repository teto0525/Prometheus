using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    //ȸ�� �ӵ� ���� 
    public float rotSpeed = 200f;

    //ȸ���� ���� 
    float mx = 0;

    public bool useMouse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���� ���°� '������' ������ �븸 ������ �� �հ� �Ѵ�. 
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        Vector2 stickpos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);


        //������� ���콺 �Է��� �޾� �÷��̾ ȸ����Ű�� �ʹ�. 
        //1. ���콺 �¿� �Է°��� �޴´�. 
        float mouse_X;
        if(useMouse == true)
        {
            mouse_X = Input.GetAxis("Mouse X");
        }
        else
        {
            mouse_X = stickpos.x;
        }

        //1-1 ȸ�� �� ������ ���콺 �Է� �� ��ŭ �̸� ���� ��Ų��. 
        mx += mouse_X * rotSpeed * Time.deltaTime;

        //2. ȸ�� �������� ��ü�� ȸ����Ų��. 
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
