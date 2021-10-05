using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    /* �⺻ ���� */
    [Header("General")]
    // Ÿ����ġ
    private Transform target;
    // ����
    public float range = 15f;
    // Ÿ�� �߽߰� �ð�, Ÿ�� ���
    float TimeStartedDetection;
    float TimeLostDetection;

    /* Public */
    [Header("Unity Setup Fields")]
    // ���� ����
    public Transform partToRotate;
    // ���� �ӵ�
    public float rotSpeed = 2;

    /* ������ */
    [Header("Use Laser")]
    public bool useLaser = false;
    public Transform Firepos;
    public GameObject prefabs;
    public float MaxLength;
    private Ray ray;
    private Vector3 direction;
    private Quaternion rotation;
    private EGA_Laser LaserScript;
    public LineRenderer lineRenderer;
    //public ParticleSystem[] lineVfx;
    //public ParticleSystem impatctVfx;

    /* ����Ʈ */
    [Tooltip("Random Damage Effect")]
    // �ǰݽ�
    public ParticleSystem[] RamdomhitSparks;
    // �÷��̾� �߽߰�
    public ParticleSystem[] OnDectVfx;
    public AudioClip OnDectSfx;


    [Tooltip("Explosion")]
    public GameObject exploVFX;
    public float exploRadius = 1.0f;
    Rigidbody rb;


    [Tooltip("Damaged")]
    int hitCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player")?.GetComponent<Transform>();

        // ������ ��ũ��Ʈ Ȱ��ȭ
        LaserScript = Instantiate(prefabs).GetComponent<EGA_Laser>();
        LaserScript.gameObject.SetActive(false);
    }




    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SoundManager.soundManager.PlaySFX(SoundManager.SFX.EnemyDetection);
        //}

        // Ÿ���� �Ÿ�
        float distance = Vector3.Distance(target.transform.position, transform.position);
        //���� Ÿ���� ���� ������ ������
        if(distance <= range)
        {
            OnDetectedTarget();
        }
        if (distance > range)
        {
            OnLostTarget();
        }
    }


    bool isFind;
    /* Ÿ�� �߰� */
    void OnDetectedTarget()
    {
        
        // Ÿ�� �������� ����
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotSpeed).eulerAngles;
        // y�ุ ȸ��
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        // �������� ����ϰڴ�
        useLaser = true;
        //���� �������� ����ϸ�
        if (useLaser)
        {
            //������
            Laser();
        }

        if (isFind == false)
        {
            // �÷��̾� �߰� ȿ��
            OnDectVfx[0].Play();
            isFind = true;
            print("��ƼŬ �÷���!!");

            if (OnDectSfx)
            {
                SoundManager.soundManager.PlaySFX(SoundManager.SFX.EnemyDetection);

            }
        }

       

        TimeStartedDetection = Time.time;
    }

    

    /* �ǰ� ȿ�� */
    private void OnTriggerEnter(Collider coll)
    {
        hitCount++;

        if (RamdomhitSparks.Length > 0)
        {
            int n = Random.Range(0, RamdomhitSparks.Length - 1);
            RamdomhitSparks[n].Play();
        }

        // ���� 10���� ������ �ı��ȴ�
        if (hitCount == 10)
        {
            Instantiate(exploVFX, transform.position, Quaternion.identity);
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            LaserScript.gameObject.SetActive(false);
            SoundManager.soundManager.PlaySFX(SoundManager.SFX.TurretExplo);
            Destroy(gameObject);
        }
    }

    /* ������ ���� */
    void Laser()
    {
        //if (!lineRenderer.enabled)
        //{
        //    lineRenderer.enabled = true;
        //    //lineVfx[0].Play();
        //}

        // ������ ��ũ��Ʈ Ȱ��ȭ
        //LaserScript = Instantiate(prefabs, Firepos.transform.position, Firepos.transform.rotation).GetComponent<EGA_Laser>();
        LaserScript.gameObject.SetActive(true);
        LaserScript.transform.position = Firepos.transform.position;
        LaserScript.transform.rotation = Firepos.transform.rotation;
        //// ������ FirePos
        //lineRenderer.SetPosition(0, Firepos.position);
        ////���� Ÿ��
        //lineRenderer.SetPosition(1, target.position);

        Vector3 dir = Firepos.position - target.position;
        //impatctVfx.transform.position = target.position + dir.normalized;
        //mpatctVfx.transform.rotation = Quaternion.LookRotation(dir);


    }

    /* Ÿ�� ��� */
    void OnLostTarget()
    {
        OnDectVfx[0].Stop();
        LaserScript.gameObject.SetActive(false);
        TimeLostDetection = Time.time;
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
