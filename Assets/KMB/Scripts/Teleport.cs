using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //왼손의 Transform
    public Transform trLeft;
    //이동할 위치
    Vector3 movePoint;


    public Transform start;
    public Transform end;

    public Transform lighting;

    void Start()
    {

    }

    void Update()
    {

        //만약에 왼손의 트리거버튼을 누르고 있으면
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            //왼손위치, 왼손 앞방향에서 나가는 Ray만든다
            Ray ray = new Ray(trLeft.position, trLeft.forward);
            //만약에 어딘가에 부딪혔다면
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //line.gameObject.SetActive(true);
                lighting.gameObject.SetActive(true);
                //왼손위치, 부딪힌위치까지 Line을 그린다
                //0 : 왼손위치, 1 : 부딪힌위치
                //line.DrawLine(trLeft.position, hit.point);
                start.position = trLeft.position;
                end.position = hit.point;
                //line.SetPosition(trLeft.position, hit.point);
                //부딪힌 위치를 잠시 저장
                movePoint = hit.point;
            }
            else
            {
                //line.gameObject.SetActive(false);
                lighting.gameObject.SetActive(false);
            }
        }

        //만약에 왼손의 트리거버튼을 떼면
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            //만약에 line이 활성화가 되어있다면
            if (lighting.gameObject.activeSelf)
            {
                //line.gameObject.SetActive(false);
                lighting.gameObject.SetActive(false);
                //나를 부딪힌 위치로 이동
                //transform.position = movePoint;
            }
        }
    }
}
