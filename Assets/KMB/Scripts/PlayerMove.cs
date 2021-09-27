using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
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
        if(cc.collisionFlags == CollisionFlags.Below)
        {
            //���� ���� ���̾��ٸ� 
            if(isJumping)
            {
                //���� �� ���·� �ʱ�ȭ�Ѵ�. 
                isJumping = false;
                //ĳ���� ���� �ӵ��� O���� �����. 
                yVelocity = 0;
            }
        }

        //2-3 ���� Ű����[space]Ű�� ���ȴٸ� 
        if(Input.GetButtonDown("Jump") && !isJumping)
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

    }
}
