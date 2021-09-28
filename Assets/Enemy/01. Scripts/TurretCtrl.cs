using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    // Ÿ����ġ
    private Transform target;

    [Header("General")]
    // ����
    public float range = 15f;

    /* �Ѿ� */
    [Header("Use Bullets (default)")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    /* ������ */
    [Header("Use Laser")]
    // ������ 
    public bool useLaser = false;
    // ������ �ѱ�
    public Transform Firepos;
    public LineRenderer lineRenderer;
    public GameObject buildEffect;


    [Header("Unity Setup Fields")]
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
            // �������� ����ϰڴ�
            useLaser = true;

            // ���� �������� ����ϸ�
            if (useLaser)
            {
                // ������
                Laser();
            }
            
        }

    }

    /* ������ ���� */
    void Laser()
    {
        // ������ FirePos
        lineRenderer.SetPosition(0, Firepos.position);
        // ���� Ÿ��
        lineRenderer.SetPosition(1, target.position);

        //// ������ �߻� = �ѱ����� ������ �߻�
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
