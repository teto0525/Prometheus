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
    public int maxHp = 100;
    public int currHp;
    // 죽음, 공격선언
    bool isAttack = false;
    bool isDie = false;
    // 피격
    private GameObject hitEffect;


    // Start is called before the first frame update
    void Start()
    {
        // 필요속성 가져오기
        cc = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // 위치 할당
        enemyTr = GetComponent<Transform>();
        targetTr = GameObject.FindWithTag("Player")?.GetComponent<Transform>();

        // 현 hp 초기화
        currHp = maxHp;

        // 피격효과
        // hitEffect = 

        // 상태머신 체크
        StartCoroutine(CheckState());
        StartCoroutine(ActionState());
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

                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BULLET"))
        {
            // 충돌 총알 삭제
            Destroy(other.gameObject);
            // 피격 액션
            anim.SetTrigger("Hit");
        }
    }



}
