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

    //Player 체력 변수 
    public int hp = 100;

    //최대 체력변수
    int maxHP = 100;

    //hp 슬라이더 변수 
    public Slider hpSlider;

    public GameObject hiteffect;

    public float moveSpeed = 7f;
    //캐릭터 콘트롤러 변수 
    CharacterController cc;

    //중력변수 
    float gravity = -20;

    //수직 속력 변수
    float yVelocity;

    //점프력 변수
    public float jumpPower = 4f;

    //점프력 상태 변수 
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
        //캐릭터 콘트롤러 컴포넌트를 받아오기
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //WASD키를 누르면 입력하면 캐릭터를 그 방향으로 이동시키고 싶다. 
        //[space]키를 누르면 캐릭터를 수직으로 점프시키고 싶다. 

        //1.사용자의 입력을 받는다. 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //2.이동방향을 설정한다. 
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        //2-1 이동방향을 설정한다. 
        dir = Camera.main.transform.TransformDirection(dir);



        //2-2 만일 바닥에 착지했다면
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            //만일 점프 중이었다면 
            if (isJumping)
            {
                //점프 전 상태로 초기화한다. 
                isJumping = false;
                //캐릭터 수직 속도를 O으로 만든다. 
                yVelocity = 0;
            }
        }

        //2-3 만일 키보드[space]키를 눌렸다면 
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            //캐릭터 수직 속도에 점프력을 적용한다. 
            isJumping = true;
            yVelocity = jumpPower;
        }

        //2-4 캐릭터 수직 속도에 중력 값을 적용한다. 
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //3. 이동속도에 맞춰 이동한다. 
        cc.Move(dir * moveSpeed * Time.deltaTime);

        //4. 현재 플레이어 hp(%)를 hp 슬라이더의 value에 반영한다. 
        hpSlider.value = (float)hp / (float)maxHP;

        if (m_ladder)
        {
            bool upKey = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.J);
            bool downkey = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.K);

            //만약에 올라가는 키를 누르면
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