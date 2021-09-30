using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLine : MonoBehaviour
{
    LineRenderer line;

    //Á¡ 3°³
    public Transform p1;
    public Transform p2;
    public Transform p3;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawBezier();
    }

    public void SetPosition(Vector3 pos1, Vector3 pos3)
    {
        p1.position = pos1;
        p3.position = pos3;

        Vector3 p = Vector3.Lerp(pos1, pos3, 0.5f);
        p.y = pos1.y;
        p2.position = p;
    }

    void DrawBezier()
    {
        int num = 10;

        line.positionCount = num + 1;

        float ratio = 1.0f / num;

        for (int i = 0; i < num; i++)
        {
            Vector3 p12 = Vector3.Lerp(p1.position, p2.position, ratio * i);
            Vector3 p23 = Vector3.Lerp(p2.position, p3.position, ratio * i);
            Vector3 p = Vector3.Lerp(p12, p23, ratio * i);
            line.SetPosition(i, p);
        }

        line.SetPosition(num, p3.position);

    }

    public void DrawLine(Vector3 p1, Vector3 p2)
    {
        line.SetPosition(0, p1);
        line.SetPosition(1, p2);
    }

}
