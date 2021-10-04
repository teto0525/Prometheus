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
    public int Hp = 100;
    private Dissolve DissolveScript;
    // ����, ���ݼ���
    bool isAttack = false;
    bool isDie = false;
    // �ǰ�
    public GameObject hitEffect;


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


        // ���¸ӽ� üũ
        StartCoroutine(CheckState());
        StartCoroutine(ActionState());
    }

    private void Update()
    {
        //���� ���°� '������' ������ �븸 ������ �� �ְ� �Ѵ�. 
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }
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

            // �׾��ٸ� �ڷ�ƾ�� �����Ѵ�
            if (state == State.Death) yield break;

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
                    // ���� ����
                    agent.isStopped = true;
                    // ��� �ִϸ��̼� ����
                    anim.SetTrigger("Die");

                    GetComponent<Dissolve>().Show();

                    //// ������
                    //DissolveScript.gameObject.SetActive(true);
                    //// �� ���ֱ�
                    //Destroy(gameObject, 2.5f);
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }

    }

    private void OnTriggerEnter(Collider coll)
    {
        // �ʴɷ� �ǰ�
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // hp ����
            Hp -= 40;
            if (Hp <= 0)
            {
                state = State.Death;
            }
        }

        // �ѱ� �ǰ�
        if (coll.CompareTag("BULLET"))
        {

            // �浹 �Ѿ� ����
            Destroy(coll.gameObject);
            // �ǰ� �׼�
            anim.SetTrigger("Hit");

            ///// �ǰ� ����Ʈ ���� /////
            // �Ѿ��� �浹 ����
            //Vector3 pos = coll.GetContact(0).point;
            //// �浹�������� ����Ʈ �����ϱ�
            //Quaternion rot = Quaternion.LookRotation(-coll.GetContact(0).normal);
            //GameObject sparks = Instantiate<GameObject>(hitEffect, pos, rot, enemyTr);
            //Destroy(sparks, 0.3f);

            // hp ����
            Hp -= 10;

            // ���ó��
            if (Hp <= 0)
            {
                state = State.Death;
            }

        }

    }


    /* ���� �����Ÿ� ǥ��*/
    private void OnDrawGizmos()
    {
        // ���� �����Ÿ� ǥ��
        if(state == State.Move)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDistance);
        }
        // ���� �����Ÿ� ǥ��
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
    // //���� �÷��̾ ���� ���� �̳��� �ִٸ� �÷��̾ �����Ѵ�.
    // if(Vector3.Distance(transform.position, player.position) < attackDistance)
    //    {
    //        //�����ð����� �÷��̾ �����Ѵ�. 
    //        currentTime += Time.deltaTime;

    //        if (currentTime > attackDelay)
    //        {
    //            player.GetComponent<PlayerMove>().DamangeAction(attackPower);
    //            print("����");
    //            currentTime = 0; 
    //        }
    //    }

    //}

}
