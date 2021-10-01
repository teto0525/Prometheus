using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMove : MonoBehaviour
{
    private bool m_ladder, m_air;

    private void start()
    {
        m_ladder = false;
        m_air = false;
    }

    //Player ü�� ���� 
    public int hp = 100;

    //�ִ� ü�º���
    int maxHP = 100;

    //hp �����̴� ���� 
    public Slider hpSlider;

    public GameObject hiteffect;

    public float moveSpeed = 7f;
    //ĳ���� ��Ʈ�ѷ� ���� 
    CharacterController cc;

    //�߷º��� 
    float gravity = -20;

    //���� �ӷ� ����
    float yVelocity;

    //������ ����
    public float jumpPower = 4f;

    //������ ���� ���� 
    public bool isJumping = false;

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

    // Start is called before the first frame update
    void Start()
    {
        //ĳ���� ��Ʈ�ѷ� ������Ʈ�� �޾ƿ���
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //WASDŰ�� ������ �Է��ϸ� ĳ���͸� �� �������� �̵���Ű�� �ʹ�. 
        //[space]Ű�� ������ ĳ���͸� �������� ������Ű�� �ʹ�. 

        //1.������� �Է��� �޴´�. 
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
                //ĳ���� ���� �ӵ��� O���� �����. 
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

        if (m_ladder)
        {
            bool upKey = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.J);
            bool downkey = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.K);

            //���࿡ �ö󰡴� Ű�� ������
            if (upKey)
            {
                this.transform.Translate(0, moveSpeed * Time.deltaTime, 0);
            }
            else if (downkey)
            {
                this.transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
            }

        }
    }
    void OnTriggerEnter(Collider Get)
    {
        if (Get.transform.tag == "Ladder-bottom")
        {
            if (!m_ladder)
            {
                m_ladder = true;
                this.transform.Translate(0, 0.5f, 0);
            }
        }
        else if (Get.transform.tag == "Ladder-air")
        {
            if (m_ladder)
            {
                m_ladder = false;
                m_air = true;
            }
        }
        else if (Get.transform.tag == "Ladder-top")
        {
            if (!m_ladder)
            {
                m_ladder = true;

                this.transform.Translate(0, -0.5f, 0);
            }
        }
        else if (Get.transform.tag == "Ladder-floor")
        {
            if (m_ladder)
            {
                m_ladder = false;
            }
        }
    }

    void OnTriggerExit(Collider Get)
    {
        if (Get.transform.tag == "Ladder-air")
        {
            m_air = false;
        }
    }
}