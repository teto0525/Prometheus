using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayserPointer : MonoBehaviour
{
    // ������
    private LineRenderer layser;
    // ������ ����Ʈ �����Ÿ�
    public float raycastDistance = 100;

    // �浿�� ��ü
    private RaycastHit collidObj;
    // ���� �ֱٿ� �浹�� ��ü
    private GameObject currentObj;

    // Start is called before the first frame update
    void Start()
    {
        layser = this.gameObject.AddComponent<LineRenderer>();

        // ���� ����
        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 255, 0.5f);
        layser.material = material;

        // ������
        layser.positionCount = 2;

        // ����
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
            // �浹����
            layser.SetPosition(1, collidObj.point);

            // �浹 ��ü�� �±װ� ��ư�� ���
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

        // �ε����� ���ٸ� ������ �ʱ� ������� ���
        else
        {
            layser.SetPosition(1, transform.position + (transform.forward * raycastDistance));

            // �ֱ� ������ ������Ʈ�� ��ư�� ���
            // ��ư�� ���� �����ִ� �����̹Ƿ� Ǯ���ֱ�
            if(currentObj != null)
            {
                currentObj.GetComponent<Button>().OnPointerExit(null);
                currentObj = null;
            }
        }
    }

    private void LateUpdate()
    {
        // ��ư ���� ���
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            layser.material.color = new Color(255, 255, 255, 0.5f);
        }

        // ��ư �����
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            layser.material.color = new Color(0, 195, 255, 0.5f);
        }
    }
}
