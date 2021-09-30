using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //�޼��� Transform
    public Transform trLeft;
    //�̵��� ��ġ
    Vector3 movePoint;


    public Transform start;
    public Transform end;

    public Transform lighting;

    void Start()
    {

    }

    void Update()
    {

        //���࿡ �޼��� Ʈ���Ź�ư�� ������ ������
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            //�޼���ġ, �޼� �չ��⿡�� ������ Ray�����
            Ray ray = new Ray(trLeft.position, trLeft.forward);
            //���࿡ ��򰡿� �ε����ٸ�
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //line.gameObject.SetActive(true);
                lighting.gameObject.SetActive(true);
                //�޼���ġ, �ε�����ġ���� Line�� �׸���
                //0 : �޼���ġ, 1 : �ε�����ġ
                //line.DrawLine(trLeft.position, hit.point);
                start.position = trLeft.position;
                end.position = hit.point;
                //line.SetPosition(trLeft.position, hit.point);
                //�ε��� ��ġ�� ��� ����
                movePoint = hit.point;
            }
            else
            {
                //line.gameObject.SetActive(false);
                lighting.gameObject.SetActive(false);
            }
        }

        //���࿡ �޼��� Ʈ���Ź�ư�� ����
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            //���࿡ line�� Ȱ��ȭ�� �Ǿ��ִٸ�
            if (lighting.gameObject.activeSelf)
            {
                //line.gameObject.SetActive(false);
                lighting.gameObject.SetActive(false);
                //���� �ε��� ��ġ�� �̵�
                //transform.position = movePoint;
            }
        }
    }
}
