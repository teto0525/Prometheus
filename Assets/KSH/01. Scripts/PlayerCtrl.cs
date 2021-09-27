using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// WASD�� �����̰� MouseX �� �޾Ƽ� ȸ���ϴ� ĳ����
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    
public class PlayerCtrl : MonoBehaviour
{
    private Camera mCamera;
    private CharacterController cc;
    private Vector3 originCameraPosition;
    private new AudioSource audio;



    // Start is called before the first frame update
    void Start()
    {
        // ĳ���� ��Ʈ�ѷ�
        cc = GetComponent<CharacterController>();
        // ī�޶�
        mCamera = Camera.main;
        originCameraPosition = mCamera.transform.localPosition;
        // �����
        audio = GetComponent<AudioSource>();
    }

    // ������ ����
    // �ʿ�Ӽ� : ���ǵ�, �Ͻ��ǵ�, ������
    // ������
    private float h;
    private float v;
    private float r;
    // ���ǵ�
    public float movespeed = 2f;
    public float turnspeed = 10f;
    void Update()
    {
        // �÷��̾� ������ ����
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");

        Vector3 dir = ((Vector3.right * h) + (Vector3.forward * v));
        transform.Translate(dir.normalized * movespeed * Time.deltaTime);
        transform.Rotate(Vector3.up * turnspeed * r * Time.deltaTime);


    }
}
