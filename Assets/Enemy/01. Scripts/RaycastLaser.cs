using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastLaser : MonoBehaviour
{
    
    //2#
    public GameObject Raybody; //����ĳ������ ��� ��ġ
    public GameObject ScaleDistance; //�Ÿ��� ���� ������ ��ȭ�� ���� ������Ʈ ���
    public GameObject RayResult; //�浹�ϴ� ��ġ�� ����� ���

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //����ĳ���� ��������� hit��� �̸����� ���Ѵ�.
        RaycastHit hit;

        //����ĳ��Ʈ ��� ��ġ, ����, �����, �ִ��νİŸ�
        Physics.Raycast(transform.position, transform.forward, out hit, 200);

        //�Ÿ��� ���� ������ ������ ��ȭ
        ScaleDistance.transform.localScale = new Vector3(1, hit.distance, 1);

        //����ĳ��Ʈ�� ���� ���� ������Ʈ�� �ű��.
        RayResult.transform.position = hit.point;

        //�ش��ϴ� ������Ʈ�� ȸ������ ���� ������ ��ֹ���� ��ġ��Ų��.
        RayResult.transform.rotation = Quaternion.LookRotation(hit.normal);
    }
}


