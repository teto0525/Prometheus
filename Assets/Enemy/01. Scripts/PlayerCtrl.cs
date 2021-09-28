using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// WASD로 움직이고 MouseX 값 받아서 회전하는 캐릭터
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
        // 캐릭터 컨트롤러
        cc = GetComponent<CharacterController>();
        // 카메라
        mCamera = Camera.main;
        originCameraPosition = mCamera.transform.localPosition;
        // 오디오
        audio = GetComponent<AudioSource>();
    }

    // 움직임 구현
    // 필요속성 : 스피드, 턴스피드, 움직임
    // 움직임
    private float h;
    private float v;
    private float r;
    // 스피드
    public float movespeed = 2f;
    public float turnspeed = 10f;
    void Update()
    {
        // 플레이어 움직임 구현
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");

        Vector3 dir = ((Vector3.right * h) + (Vector3.forward * v));
        transform.Translate(dir.normalized * movespeed * Time.deltaTime);
        transform.Rotate(Vector3.up * turnspeed * r * Time.deltaTime);


    }
}
