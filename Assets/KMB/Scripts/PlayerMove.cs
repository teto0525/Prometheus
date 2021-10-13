using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMove : MonoBehaviour
{
    // �̱���
    public static PlayerMove pm;

    private void Awake()
    {
        if (pm == null)
            pm = this;
    }

    private Vector3 moveDirection = Vector3.zero;

    //Player ü�� ����
    public float hp = 100;

    //�ִ� ü�º���
    float maxHP = 100;

    //hp �����̴� ����
    public Slider hpSlider;

    public GameObject hiteffect;

    public float moveSpeed = 7f;

    //ĳ���� ��Ʈ�ѷ� ����
    CharacterController cc;
    Rigidbody rigid;

    //�߷º���
    float gravity = -20;

    //���� �ӷ� ����
    float yVelocity;

    //������ ����
    public float jumpPower = 4f;

    //������ ���� ����
    public bool isJumping = false;

    /* KSH  �������� */
    // ����ĳ��Ʈ
    private Ray ray;
    private RaycastHit hit;
    int layerMask = 1 << LayerMask.NameToLayer("Capsule");


    // Start is called before the first frame update
    void Start()
    {
        //ĳ���� ��Ʈ�ѷ� ������Ʈ�� �޾ƿ���
        cc = GetComponent<CharacterController>();


    }

    public void DamangeAction(int damage)
    {
        hp -= damage;
        if (hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }

    }
    IEnumerator PlayHitEffect()
    {
        hiteffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        hiteffect.SetActive(false);
    }


    /* KSH �������� */
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == ("AttackPoint"))
        {
            DamangeAction(5);

            print(hp);
        }

        else if (other.tag == ("BulletUp"))
        {
            RecoverAction(10);
            Destroy(other.gameObject);

            print(hp);
        }

    }

    public void RecoverAction(float damage)
    {
        hp += damage;
        print(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        //WASDŰ�� ������ �Է��ϸ� ĳ���͸� �� �������� �̵���Ű�� �ʹ�.
        //[space]Ű�� ������ ĳ���͸� �������� ������Ű�� �ʹ�.

        //1.�������� �Է��� �޴´�.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //2.�̵������� �����Ѵ�.
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        //2-1 �̵������� �����Ѵ�.
        dir = Camera.main.transform.TransformDirection(dir);

        //2-2 ���� �ٴڿ� �����ߴٸ�
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            //���� ���� ���̾��ٸ�
            if (isJumping)
            {
                //���� �� ���·� �ʱ�ȭ�Ѵ�.
                isJumping = false;
                //ĳ���� ���� �ӵ��� O���� ������.
                yVelocity = 0;
            }
        }

        //2-3 ���� Ű����[space]Ű�� ���ȴٸ�
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            //ĳ���� ���� �ӵ��� �������� �����Ѵ�.
            isJumping = true;
            yVelocity = jumpPower;
        }

        //2-4 ĳ���� ���� �ӵ��� �߷� ���� �����Ѵ�.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //3. �̵��ӵ��� ���� �̵��Ѵ�.
        cc.Move(dir * moveSpeed * Time.deltaTime);

        //4. ���� �÷��̾� hp(%)�� hp �����̴��� value�� �ݿ��Ѵ�.
        hpSlider.value = (float)hp / (float)maxHP;


        /* KSH �������� */
        if (SampleManager.sm.popUps != null )
        {
            ray = new Ray(transform.position, transform.forward);
;           if (Physics.Raycast(ray, out hit, 100, layerMask))
            {

                // �̹��� �������� SetActive
                SampleManager.sm.PopUps();
                // �̹��� SetActive �� �ڷ� �ش� capsule ���̾� �̸� �ٲ��ֱ�
                layerMask = LayerMask.NameToLayer("Default");
            }

        }
    }

    /* KSH �������� */
    public SampleManager sm;



}
