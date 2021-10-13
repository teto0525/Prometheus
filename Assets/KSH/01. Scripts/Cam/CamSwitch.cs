using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    // 카메라 전환
    public Camera OVRCam; // 플레이어 캠
    public Camera MCam; // 모니터 캠
    bool onMonitor = false;

    // Start is called before the first frame update
    void Start()
    {
        MainCameraOn();
    }

    // 메인카메라
    void MainCameraOn()
    {

        OVRCam.enabled = true;
        MCam.enabled = false;

    }

    // 모니터 카메라
    void MonitorCamerOn()
    {

        OVRCam.enabled = false;
        MCam.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 모니터캠 구역안에 들어가면
        if (other.name == "MonitorCam")
        {
            // 카메라를 switch 하라
            MonitorCamerOn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            
            MainCameraOn();
            
        }

       
        else
        {
            //onMonitor = true;
           // MonitorCamerOn();
        }
    }
}
