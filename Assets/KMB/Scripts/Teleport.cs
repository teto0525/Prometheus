using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    //Player ü�� ���� 
    public float energy = 100;

    //�ִ� ü�º���
    float maxEn = 100;

    //Energybar �����̴� ����
    public Slider energyBar;

    //�޼��� Transform
    public Transform trLeft;
    //�̵��� ��ġ
    Vector3 movePoint;

    public Transform start;
    public Transform end;

    public Transform lighting;

    void Start()
    {

    }

    void Update()
    {

        //���࿡ �޼��� Ʈ���Ź�ư�� ������ ������
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            //�޼���ġ, �޼� �չ��⿡�� ������ Ray�����
            Ray ray = new Ray(trLeft.position, trLeft.forward);
            //���࿡ ��򰡿� �ε����ٸ�
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //line.gameObject.SetActive(true);
                lighting.gameObject.SetActive(true);
                SoundManager.soundManager.PlaySFX(SoundManager.SFX.Electric);

                //�޼���ġ, �ε�����ġ���� Line�� �׸���
                //0 : �޼���ġ, 1 : �ε�����ġ
                //line.DrawLine(trLeft.position, hit.point);
                start.position = trLeft.position;
                end.position = hit.point;
                //line.SetPosition(trLeft.position, hit.point);
                //�ε��� ��ġ�� ��� ����
                movePoint = hit.point;

                DamageAction(0.5f);

                //���࿡ �������� ���ʹ̶��
                if (hit.transform.tag == "Enemy")
                {
                    //���ʹ̸� �׿���
                    Destroy(hit.transform.gameObject, 2);
                }
            }
            else
            {
                //line.gameObject.SetActive(false);
                lighting.gameObject.SetActive(false);
            }
        }

        //���࿡ �޼��� Ʈ���Ź�ư�� ����
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            //���࿡ line�� Ȱ��ȭ�� �Ǿ��ִٸ�
            if (lighting.gameObject.activeSelf)
            {
                //line.gameObject.SetActive(false);
                lighting.gameObject.SetActive(false);
                //���� �ε��� ��ġ�� �̵�
                //transform.position = movePoint;
            }
        }
        //4. ���� �÷��̾� hp(%)�� hp �����̴��� value�� �ݿ��Ѵ�. 
        energyBar.value = (float)energy / (float)maxEn;
    }

    public void DamageAction(float damage)
    {
        energy -= damage;
        print(energy);
    }

    public void RecoverAction(float damage)
    {
        energy += damage;
        print(energy);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == ("EnergyUp"))
        {
            RecoverAction(10);
            Destroy(other.gameObject);
            SoundManager.soundManager.PlaySFX(SoundManager.SFX.EnergyBar);

            print(energy);
        }
    }
}
