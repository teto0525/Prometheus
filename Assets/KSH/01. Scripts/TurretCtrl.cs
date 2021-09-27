using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    // Ÿ����ġ
    private Transform target;
    // ����
    public float range = 15f;
    public string playerTag = "Player";
    // ���� ����
    public Transform partToRotate;
    // ���� �ӵ�
    public float rotSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Ÿ�� Ư���ϱ�
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void Update()
    {
        // Ÿ���� �Ÿ�
        float distance = Vector3.Distance(target.transform.position, transform.position);
        //���� Ÿ���� ���� ������ ������
        if(distance <= range)
        {
            // Ÿ�� �������� ����
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotSpeed).eulerAngles;
            // y�ุ ȸ��
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
