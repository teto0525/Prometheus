using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class EnemyCtrl : MonoBehaviour
{
    /* 유한상태 머신 */
    public enum State
    {
        Idle,
        Move,
        Attack,
        Death
    }
    // 현재 상태 체크
    public State state = State.Idle;

    /* 필요속성 */
    // 추적 대상, 추적 당사자
    private Transform targetTr;
    private Transform enemyTr;
    // 네비게이션
    private NavMeshAgent agent;
    // 애니메이션
    private Animator anim;
    // 캐릭터 컨트롤러
    CharacterController cc;
    //RayCast 
    RaycastHit hit;
    // HP
    public int Hp = 100;
    private Dissolve DissolveScript;
    // 죽음, 공격선언
    bool isAttack = false;
    bool isDie = false;
    // 피격
    public GameObject hitEffect;


    // Start is called before the first frame update
    void Start()
    {
        ////게임 상태가 '게임중' 상태일 대만 조작할 수 잇게 한다. 
        //if (GameManager.gm.gState != GameManager.GameState.Run)
        //{
        //    return;
        //}

        // 필요속성 가져오기
        cc = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // 위치 할당
        enemyTr = GetComponent<Transform>();
        targetTr = GameObject.FindWithTag("Player")?.GetComponent<Transform>();


        // 상태머신 체크
        StartCoroutine(CheckState());
        StartCoroutine(ActionState());
    }

    private void Update()
    {
        //게임 상태가 '게임중' 상태일 대만 조작할 수 있게 한다. 
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }
    }


    // 추적 시작할 거리
    public float traceDistance = 5;
    // 공격 시작할 거리
    public float attackDistance = 2;
    IEnumerator CheckState()
    {

        while (!isDie) //안 죽었다면
        {
            yield return new WaitForSeconds(0.3f);

            // 죽었다면 코루틴을 종료한다
            if (state == State.Death) yield break;

            // target과 enemy 사이의 거리를 측정한다
            float distance = Vector3.Distance(targetTr.transform.position, agent.transform.position);

            // 추적 사정거리로 들어왔는지 확인한다
            if (distance <= attackDistance)
            {
                state = State.Attack;
            }
            // 공격 사정거리로 들어왔는지 확인한다
            else if (distance <= traceDistance)
            {
                state = State.Move;
            }
            // 그렇지 않다면 대기
            else
            {
                state = State.Idle;
            }

        }

    }


    // Update is called once per frame
    IEnumerator ActionState()
    {
        // 현 상태 프린트
        print("State :" + state);

        while(!isDie)
        {
            // 상태 업데이트
            switch (state)
            {
                /////IDLE//////
                case State.Idle:

                    //추적 중지
                    agent.isStopped = true;
                    anim.SetBool("IsTrace", false);
                    break;

                /////MOVE//////
                case State.Move:
                    // 추격을 시작한다
                    agent.isStopped = false;
                    agent.speed = 5f;
                    // 플레이어를 향해 이동한다
                    agent.SetDestination(targetTr.transform.position);
                    anim.SetBool("IsTrace", true);
                    anim.SetBool("IsAttack", false);
                    break;

                /////ATTACK//////
                case State.Attack:
                    isAttack = true;
                    anim.SetBool("IsAttack", true);
                    break;

                /////Death//////
                case State.Death:
                    isDie = true;
                    // 추적 정지
                    agent.isStopped = true;
                    // 사망 애니메이션 실행
                    anim.SetTrigger("Die");

                    GetComponent<Dissolve>().Show();

                    //// 디졸브
                    //DissolveScript.gameObject.SetActive(true);
                    //// 적 없애기
                    //Destroy(gameObject, 2.5f);
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }

    }

    private void OnTriggerEnter(Collider coll)
    {
        // 초능력 피격
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // hp 구현
            Hp -= 40;
            if (Hp <= 0)
            {
                state = State.Death;
            }
        }

        // 총구 피격
        if (coll.CompareTag("BULLET"))
        {

            // 충돌 총알 삭제
            Destroy(coll.gameObject);
            // 피격 액션
            anim.SetTrigger("Hit");

            ///// 피격 이펙트 실행 /////
            // 총알의 충돌 지점
            //Vector3 pos = coll.GetContact(0).point;
            //// 충돌지점에서 이펙트 실행하기
            //Quaternion rot = Quaternion.LookRotation(-coll.GetContact(0).normal);
            //GameObject sparks = Instantiate<GameObject>(hitEffect, pos, rot, enemyTr);
            //Destroy(sparks, 0.3f);

            // hp 구현
            Hp -= 10;

            // 사망처리
            if (Hp <= 0)
            {
                state = State.Death;
            }

        }

    }


    /* 공격 사정거리 표시*/
    private void OnDrawGizmos()
    {
        // 추적 사정거리 표시
        if(state == State.Move)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDistance);
        }
        // 공격 사정거리 표시
        if(state == State.Attack)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }

    //float currentTime;
    //public int attackPower = 3;
    //float attackDelay;
    //Transform player;

    //void Attack()
    //{
    // //만일 플레이어가 공격 범위 이내에 있다면 플레이어를 공격한다.
    // if(Vector3.Distance(transform.position, player.position) < attackDistance)
    //    {
    //        //일정시간마다 플레이어를 공격한다. 
    //        currentTime += Time.deltaTime;

    //        if (currentTime > attackDelay)
    //        {
    //            player.GetComponent<PlayerMove>().DamangeAction(attackPower);
    //            print("공격");
    //            currentTime = 0; 
    //        }
    //    }

    //}

}
