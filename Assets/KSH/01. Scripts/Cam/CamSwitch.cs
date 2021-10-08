using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    // ī�޶� ��ȯ
    public Camera OVRCam; // �÷��̾� ķ
    public Camera MCam; // ����� ķ
    bool onMonitor = false;

    // Start is called before the first frame update
    void Start()
    {
        MainCameraOn();
    }

    // ����ī�޶�
    void MainCameraOn()
    {

        OVRCam.enabled = true;
        MCam.enabled = false;

    }

    // ����� ī�޶�
    void MonitorCamerOn()
    {

        OVRCam.enabled = false;
        MCam.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����ķ �����ȿ� ����
        if (other.name == "MonitorCam")
        {
            // ī�޶� switch �϶�
            MonitorCamerOn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if(MCam == true)
            {
                onMonitor = false;
                MainCameraOn();
            }
        }
        else
        {
            onMonitor = true;
            MonitorCamerOn();
        }
    }
}
