using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class EnemyCtrl : MonoBehaviour
{
    /* ���ѻ��� �ӽ� */
    public enum State
    {
        Idle,
        Move,
        Attack,
        Death
    }
    // ���� ���� üũ
    public State state = State.Idle;

    /* �ʿ�Ӽ� */
    // ���� ���, ���� �����
    private Transform targetTr;
    private Transform enemyTr;
    // �׺���̼�
    private NavMeshAgent agent;
    // �ִϸ��̼�
    private Animator anim;
    // ĳ���� ��Ʈ�ѷ�
    CharacterController cc;
    //RayCast 
    RaycastHit hit;
    // HP
    public int maxHp = 100;
    public int currHp;
    // ����, ���ݼ���
    bool isAttack = false;
    bool isDie = false;
    // �ǰ�
    private GameObject hitEffect;


    // Start is called before the first frame update
    void Start()
    {
        // �ʿ�Ӽ� ��������
        cc = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // ��ġ �Ҵ�
        enemyTr = GetComponent<Transform>();
        targetTr = GameObject.FindWithTag("Player")?.GetComponent<Transform>();

        // �� hp �ʱ�ȭ
        currHp = maxHp;

        // �ǰ�ȿ��
        // hitEffect = 

        // ���¸ӽ� üũ
        StartCoroutine(CheckState());
        StartCoroutine(ActionState());
    }

    // ���� ������ �Ÿ�
    public float traceDistance = 5;
    // ���� ������ �Ÿ�
    public float attackDistance = 2;
    IEnumerator CheckState()
    {
        while (!isDie) //�� �׾��ٸ�
        {
            yield return new WaitForSeconds(0.3f);

            // target�� enemy ������ �Ÿ��� �����Ѵ�
            float distance = Vector3.Distance(targetTr.transform.position, agent.transform.position);

            // ���� �����Ÿ��� ���Դ��� Ȯ���Ѵ�
            if (distance <= attackDistance)
            {
                state = State.Attack;
            }
            // ���� �����Ÿ��� ���Դ��� Ȯ���Ѵ�
            else if (distance <= traceDistance)
            {
                state = State.Move;
            }
            // �׷��� �ʴٸ� ���
            else
            {
                state = State.Idle;
            }

        }

    }


    // Update is called once per frame
    IEnumerator ActionState()
    {
        // �� ���� ����Ʈ
        print("State :" + state);

        while(!isDie)
        {
            // ���� ������Ʈ
            switch (state)
            {
                /////IDLE//////
                case State.Idle:

                    //���� ����
                    agent.isStopped = true;
                    anim.SetBool("IsTrace", false);
                    break;

                /////MOVE//////
                case State.Move:
                    // �߰��� �����Ѵ�
                    agent.isStopped = false;
                    agent.speed = 5f;
                    // �÷��̾ ���� �̵��Ѵ�
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



    private void OnDemage()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ���� ����ĳ��Ʈ�� �¾Ҵٸ�
        if(Physics.Raycast(ray, out hit))
        {
            // �´� �������� �ǰ� ȿ�� �ֱ�
        }

    }



}
