using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public Transform trRight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //1. ī�޶� ��ġ, ī�޶� �չ������� ������ Ray�� �����. 
        //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Ray ray = new Ray(trRight.position, trRight.forward);
        //2. ���࿡ Ray�߻��ؼ� ��򰡿� �ε�����      
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //3. �ε��� ��ġ�� CrossHair(��)�� ���´�. 
            transform.position = hit.point;
            //4. Crosshair�� ũ�⸦ ī�޶󿡼� �ε��� ��ġ�� �Ÿ���ŭ ���Ѵ�. 
            transform.localScale = Vector3.one * hit.distance; // or new Vector3(1,1,1)*hit.distance
        }

    }
}
