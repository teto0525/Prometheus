using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilsonCtrl : MonoBehaviour
{
	/* 움직임 */
	// Vector3 rot = Vector3.zero;

	/* 스피드 */
	[Range(5.0f, 50.0f)]
	public float moveSpeed = 5f;
	[Range(5.0f, 50.0f)]
	public float rotSpeed = 5f;

	/* 애니메이션 */
	Animator anim;

	public enum State
    {
		IDLE,
		WALK,
		AttACK
    }
	public State pstate = State.IDLE;

	/* 목표물 = 주인공 */
	public GameObject Player;
	bool PlayerMove;

	/* 슬롯 UI */
	//생략 public GameObject slot;

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
			// 만약 플레이어가 idle 이라면 idel 상태로
			case State.IDLE:
				idle();
				break;

			// 만약 플레이어가 움직이면 walk 상태로
			case State.WALK:
				walk();
				break;

			// 만약 공격 버튼 눌려지면 attack 상태로
			case State.AttACK:
				attack();
				break;

        }
	}

    private void idle()
    {
		// 만약 플레이어가 움직이지 않는다면
		if (PlayerMove == false)
		{
			anim.SetBool("IsMove", false);
		}
	}

	// 플레이어가 움직이면 플레이어쪽으로 이동하고 싶다
    private void walk()
    {
        if(PlayerMove == true)
        {
			anim.SetBool("IsMove", true);

			// PlayerPos 방향으로 이동
			Vector3 dir = Player.transform.position - transform.position;
			dir.y = 0;
			transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);

        }
    }

	/* 공격 Effect */
	public GameObject attackEf;
	bool IsAttack;

    private void attack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
			IsAttack = true;

			// roll 준비상태 돌입하기
			anim.SetBool("IsAttack", true);
			roll();
        }
    }
	/* 애너미 공격하기 */
	void roll()
    {
        // attack()와 roll() 사이에 딜레이가 있었으면 좋겠다 --> 코루틴
        // Q 버튼 계속해서 누르면 구른다
        if (Input.GetKey(KeyCode.Q))
        {
			anim.SetBool("IsRoll", true);
			Instantiate(attackEf);

        }

    }

}
