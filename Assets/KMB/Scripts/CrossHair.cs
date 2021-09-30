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
        //1. 카메라 위치, 카메라 앞방향으로 나가는 Ray를 만든다. 
        //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Ray ray = new Ray(trRight.position, trRight.forward);
        //2. 만약에 Ray발사해서 어딘가에 부딪힌다      
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //3. 부딪힌 위치에 CrossHair(나)를 놓는다. 
            transform.position = hit.point;
            //4. Crosshair의 크기를 카메라에서 부딪힌 위치의 거리만큼 곱한다. 
            transform.localScale = Vector3.one * hit.distance; // or new Vector3(1,1,1)*hit.distance
        }

    }
}
