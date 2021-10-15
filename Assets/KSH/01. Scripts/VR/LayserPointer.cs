using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayserPointer : MonoBehaviour
{
    // 레이저
    private LineRenderer layser;
    // 레이저 포인트 감지거리
    public float raycastDistance = 100;

    // 충동된 객체
    private RaycastHit collidObj;
    // 가장 최근에 충돌한 객체
    private GameObject currentObj;

    // Start is called before the first frame update
    void Start()
    {
        layser = this.gameObject.AddComponent<LineRenderer>();

        // 라인 색상
        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 255, 0.5f);
        layser.material = material;

        // 꼭짓점
        layser.positionCount = 2;

        // 굵기
        layser.startWidth = 0.01f;
        layser.endWidth = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        layser.SetPosition(0, transform.position);

        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green, 0.5f);

        if (Physics.Raycast(transform.position, transform.forward, out collidObj, raycastDistance))
        {
            // 충돌감지
            layser.SetPosition(1, collidObj.point);

            // 충돌 객체의 태그가 버튼인 경우
            if (collidObj.collider.gameObject.CompareTag("Button"))
            {
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    collidObj.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                }

                else
                {
                    collidObj.collider.gameObject.GetComponent<Button>().OnPointerEnter(null);
                    currentObj = collidObj.collider.gameObject;
                }
            }

        }

        // 부딪힌게 없다면 레이저 초기 설정대로 길게
        else
        {
            layser.SetPosition(1, transform.position + (transform.forward * raycastDistance));

            // 최근 감지된 오브젝트가 버튼일 경우
            // 버튼은 현재 눌려있는 상태이므로 풀어주기
            if(currentObj != null)
            {
                currentObj.GetComponent<Button>().OnPointerExit(null);
                currentObj = null;
            }
        }
    }

    private void LateUpdate()
    {
        // 버튼 누를 경우
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            layser.material.color = new Color(255, 255, 255, 0.5f);
        }

        // 버튼 뗄경우
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            layser.material.color = new Color(0, 195, 255, 0.5f);
        }
    }
}
