using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //�Ѿ˰���
    public GameObject bulletFactory;
    //�ѱ�
    public Transform trFirePos;

    public GameObject fragmentFactory;

    //�޼� Transform.
    public Transform trLeft;

    

    // Start is called before the first frame update
    void Start()
    {
        trFirePos = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 stickpos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        print(stickpos.x + " , " + stickpos.y);

        ////���콺 ���� ��ư�� ������ �Ѿ��� �߻��ϰ� �ʹ�. 
        ////1.���࿡ ���콺 ������ ��ư�� ������
        //if (Input.GetButtonDown("Fire1")) //|| OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))

        //{
        //    FireBullet();
        //}

        //���콺 ������ ��ư�� ������ Ray�� Ȱ���ؼ�
        //������ ���� ȿ���� �����ش�. .
        //1. ���࿡ ���콺 ������ ��ư�� ������


        //���콺 ������ ��ư�� ������ �ü��� �ٶ󺸴� �������� ���� �߻��ϰ� �ʹ�. 
        // ���콺 ������ ��ư�� �Է¹޴´�. 


        if (Input.GetButtonDown("Fire1")  || OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            FireRay();
        }
    }


    void FireRay()
    {
        //2. ī�޶� ��ġ, ī�޶� �չ������� ������ Ray�� �����.
        Ray ray = new Ray(trFirePos.position, trFirePos.forward);
        //3. ���࿡ Ray�� �߻��ؼ� ��򰡿� �ε����ٸ�
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //4. ���� ȿ�����忡�� ������ �����. 
            GameObject fragment = Instantiate(fragmentFactory);
            //5. ���� ȿ���� �ε��� ��ġ�� ���´�. 
            fragment.transform.position = hit.point;
            //6. ����ȿ���� �� ������ �ε��� ��ġ�� normal ���ͷ� ����
            fragment.transform.forward = hit.normal;
            //7.2�ʵڿ� �ı�����. 
            Destroy(fragment, 2);

            //8. ���࿡ ���� ���� Drone �̶�� 
            //if (hit.transform.name.Contains("Enemy"))
            //{
            //    //9. �ı��ض�.
            //    Destroy(hit.transform.gameObject);
            //}


        }

    }

    //void FireBullet()
    //{
    //    //2. �Ѿ� ���忡�� �Ѿ��� �����.
    //    GameObject bullet = Instantiate(bulletFactory);
    //    //3. ������� �Ѿ��� �ѱ��� ���´�. 
    //    bullet.transform.position = trFirePos.position;
    //    //4. ������� �Ѿ��� �չ����� �ѱ��� �չ������� �Ѵ�. 
    //    bullet.transform.forward = trFirePos.forward;
    //}
}
