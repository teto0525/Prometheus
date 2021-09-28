using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilsonCtrl : MonoBehaviour
{
	/* ������ */
	// Vector3 rot = Vector3.zero;

	/* ���ǵ� */
	[Range(5.0f, 50.0f)]
	public float moveSpeed = 5f;
	[Range(5.0f, 50.0f)]
	public float rotSpeed = 5f;

	/* �ִϸ��̼� */
	Animator anim;

	public enum State
    {
		IDLE,
		WALK,
		AttACK
    }
	public State pstate = State.IDLE;

	/* ��ǥ�� = ���ΰ� */
	public GameObject Player;
	bool PlayerMove;

	/* ���� UI */
	//���� public GameObject slot;

	// Use this for initialization
	void Start()
	{
		anim = gameObject.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		switch (pstate)
        {
			// ���� �÷��̾ idle �̶�� idel ���·�
			case State.IDLE:
				idle();
				break;

			// ���� �÷��̾ �����̸� walk ���·�
			case State.WALK:
				walk();
				break;

			// ���� ���� ��ư �������� attack ���·�
			case State.AttACK:
				attack();
				break;

        }
	}

    private void idle()
    {
		// ���� �÷��̾ �������� �ʴ´ٸ�
		if (PlayerMove == false)
		{
			anim.SetBool("IsMove", false);
		}
	}

	// �÷��̾ �����̸� �÷��̾������� �̵��ϰ� �ʹ�
    private void walk()
    {
        if(PlayerMove == true)
        {
			anim.SetBool("IsMove", true);

			// PlayerPos �������� �̵�
			Vector3 dir = Player.transform.position - transform.position;
			dir.y = 0;
			transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);

        }
    }

	/* ���� Effect */
	public GameObject attackEf;
	bool IsAttack;

    private void attack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
			IsAttack = true;

			// roll �غ���� �����ϱ�
			anim.SetBool("IsAttack", true);
			roll();
        }
    }
	/* �ֳʹ� �����ϱ� */
	void roll()
    {
        // attack()�� roll() ���̿� �����̰� �־����� ���ڴ� --> �ڷ�ƾ
        // Q ��ư ����ؼ� ������ ������
        if (Input.GetKey(KeyCode.Q))
        {
			anim.SetBool("IsRoll", true);
			Instantiate(attackEf);

        }

    }

}
